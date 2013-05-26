using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Globalization;
using System.Diagnostics;

namespace CLEON_Connector
{
    public partial class Form1
    {
        // Test if parameters are properly set based on the power consumption of each operaions
        // CLEON's power consumption for each operation is the same as below
        // - Idle state (no sensing): 2.5 mA
        // - GPS is in action : 30.5 mA
        // - Writing to MicroSD : 40 mA
        // GPS requires at least 400 ms for stabilization
        // Each chunk requires at least 50 ms from GPS signal capturing to writing to MicroSD
        private int TestIfParametersAreValid()
        {
            #region variable declaration
            int iTimespanForASample;
            ulong ulTotalTimespan;
            double dTotalPowerConsumption;
            double dExpectedBatteryCapacity;
            ulong ulRequiredTimeForOperation;
            #endregion

            #region updating variables
            try
            {
                iSampleCount = int.Parse(textBox_SampleCount.Text);
                iSampleGap = int.Parse(textBox_sampleGap.Text);
                iChunkCount = int.Parse(textBox_ChunkCount.Text);
                iChunkGap = int.Parse(textBox_chunkGap.Text);
                iBatteryCapacity = int.Parse(textBox_batteryCapacity.Text);
            }
            catch
            {

            }
            #endregion

            // Sample count should be greater than 0
            if (iSampleCount < 1)
            {
                textBox_sampleGap.Enabled = true;

                textBox_SampleCount.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "Sample count should be greater than 0";

                return Errors.invalidValue;
            }
            else if (iSampleCount == 1)
            {
                textBox_sampleGap.Enabled = false;

                textBox_SampleCount.ForeColor = Color.Black;
                toolStripStatusLabel.Text = "Ready";
            }
            else
            {
                textBox_sampleGap.Enabled = true;

                textBox_SampleCount.ForeColor = Color.Black;
                toolStripStatusLabel.Text = "Ready";
            }

            // Chunk count should be greater than 0
            if (iChunkCount < 1)
            {
                textBox_ChunkCount.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "Chunk count should be greater than 0";

                return Errors.invalidValue;
            }
            else
            {
                textBox_ChunkCount.ForeColor = Color.Black;
                toolStripStatusLabel.Text = "Ready";
            }
            // Battery capacity should be greater than 0
            if (iBatteryCapacity < 1)
            {
                textBox_batteryCapacity.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "Battery capacitoy should be greater than 0";

                return Errors.invalidValue;
            }
            else
            {
                textBox_batteryCapacity.ForeColor = Color.Black;
                toolStripStatusLabel.Text = "Ready";
            }
            // Sample gap should be multiple of 1000 (meaning that seconds)
            if (iSampleGap % 1000 != 0)
            {
                textBox_sampleGap.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "Sample gap should be multiple of 1000";

                return Errors.invalidValue;
            }
            else
            {
                textBox_sampleGap.ForeColor = Color.Black;
                toolStripStatusLabel.Text = "Ready";
            }
            // Sample gap should be greater than chunk gap
            if (iSampleGap < iChunkGap)
            {
                textBox_chunkGap.ForeColor = Color.Red;
                textBox_sampleGap.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "Sample gap should be longer than chunk gap";

                return Errors.sampleGapIsShorterThanChunkGap;
            }
            else
            {
                textBox_chunkGap.ForeColor = Color.Black;
                textBox_sampleGap.ForeColor = Color.Black;
                toolStripStatusLabel.Text = "Ready";
            }
            // Chunk gap should be greater than required minimum capturing and storing time 
            if (iChunkGap < Constants.minimumTimespanForCapturingAndStoringAChunk)
            {
                textBox_chunkGap.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "Chunk gap should be longer than " + Constants.minimumTimespanForCapturingAndStoringAChunk.ToString() + " ms";

                return Errors.chunkGapIsTooShort;
            }
            else
            {
                textBox_chunkGap.ForeColor = Color.Black;
                toolStripStatusLabel.Text = "Ready";
            }

            // Sample gap should be longer than timespan of a sample
            if (iSampleGap < Constants.minimumTimespanForGPSStabilization + iChunkCount * iChunkGap + Constants.minimumTimespanForCapturingAndStoringAChunk)
            {
                textBox_sampleGap.ForeColor = Color.Red;
                textBox_ChunkCount.ForeColor = Color.Red;
                textBox_chunkGap.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "Sample gap is too short";

                return Errors.sampleGapIsShorterThanRequiredMinimum;
            }
            else
            {
                textBox_sampleGap.ForeColor = Color.Black;
                textBox_ChunkCount.ForeColor = Color.Black;
                textBox_chunkGap.ForeColor = Color.Black;
                toolStripStatusLabel.Text = "Ready";
            }

            // Timespan for a sample (calculated based on user input)
            iTimespanForASample = Constants.minimumTimespanForGPSStabilization
                                  + iChunkGap * (iChunkCount - 1)
                                  + Constants.minimumTimespanForCapturingAndStoringAChunk;

            // Total timespan for CLEON operation (calculated based on user input)
            ulTotalTimespan = (ulong)(iSampleCount * iSampleGap);

            // Total power consumption (calculated based on user input) (Unit : mAmS)
            dTotalPowerConsumption = ulTotalTimespan * Constants.powerConsumptionForIdleState
                                      + iTimespanForASample * Constants.powerConsumptionForGPSinOperation
                                      + iChunkCount * Constants.powerConsumptionForWritingToMicroSD * Constants.minimumTimespanForCapturingAndStoringAChunk;

            // Expected battery capacity (calculated based on user input) (Unit : mAh)
            dExpectedBatteryCapacity = dTotalPowerConsumption / 1000 / 60 / 60;

            // Required battery capacity should be smaller than given battery capacity
            if (dExpectedBatteryCapacity > iBatteryCapacity)
            {
                textBox_batteryCapacity.ForeColor = Color.Red;
                toolStripStatusLabel.Text = "Insufficient battery capacity";

                return Errors.insufficientBatteryCapacity;
            }
            else
            {
                textBox_batteryCapacity.ForeColor = Color.Black;
                toolStripStatusLabel.Text = "Ready";
            }

            // Convert milliseconds to minutes
            ulRequiredTimeForOperation = ulTotalTimespan / 1000 / 60;

            if ((ulRequiredTimeForOperation > 60) && (ulRequiredTimeForOperation <= 60 * 24))
            {
                int iHours = (int)(ulRequiredTimeForOperation / 60);
                int iMinutes = (int)(ulRequiredTimeForOperation % 60);

                toolStripStatusLabel.Text = "Required time ≒ " + iHours + " hours " + iMinutes + " minutes";
            }
            else if (ulRequiredTimeForOperation > (60 * 24))
            {
                int iDays = (int)(ulRequiredTimeForOperation / 60 / 24);
                int iHours = (int)(ulRequiredTimeForOperation /60) - iDays * 24;
                int iMinutes = (int)(ulRequiredTimeForOperation) - iDays * 24 * 60 - iHours * 60;

                toolStripStatusLabel.Text = "Required time ≒ " + iDays + " days " + iHours + " hours " + iMinutes + " minutes";
            }
            else
            {
                toolStripStatusLabel.Text = "Required time ≒ " + ulRequiredTimeForOperation + " minutes";
            }
            
            return Errors.NoErrors;
        }
    }
}
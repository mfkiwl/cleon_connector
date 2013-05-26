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
    public partial class Form1 : Form
    {
        #region variable declaration
        // Receive-USB-frame-count for state transition
        byte USBRecvFrameByteCount = 0;
        // Receive-USB-frame
        byte[] USBRecvFrame = new byte[Constants.serialFrameLength];

        // Send-USB-frame
        byte[] USBSsndFrame = new byte[Constants.serialFrameLength];

        // USB reception completion flag
        volatile bool bFLAG_USBFrameReceived = false;

        // Parameters
        int iSampleCount;
        int iSampleGap;
        int iChunkCount;
        int iChunkGap;
        int iBatteryCapacity;
        #endregion

        // Initialization
        public Form1()
        {
            InitializeComponent();
        }

        // On loading of Form1
        private void Form1_Load(object sender, EventArgs e)
        {
            // Request all the available ports
            string[] ports = SerialPort.GetPortNames();

            // list all the available ports
            foreach (string port in ports)
            {
                // Adding available port to combobox
                comboBox_availableComPort.Items.Add(port);
            }

            // Default com port
            comboBox_availableComPort.Text = "COM4";

            // Start receive routine
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);

            // Update user parameter variables
            iSampleCount = int.Parse(textBox_SampleCount.Text.ToString());
            iSampleGap = int.Parse(textBox_sampleGap.Text.ToString());
            iChunkCount = int.Parse(textBox_ChunkCount.Text.ToString());
            iChunkGap = int.Parse(textBox_chunkGap.Text.ToString());
            iBatteryCapacity = int.Parse(textBox_batteryCapacity.Text.ToString());

            TestIfParametersAreValid();
        }

        // Time
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Current time
            label_currentTime.Text = DateTime.Now.ToString(new CultureInfo("en-US"));

            // Current UTC time tick
            label_currentTimeTick.Text = DateTime.UtcNow.Ticks.ToString();            
        }

        // If button 'Connect' is clicked
        private void button_Connect_Click(object sender, EventArgs e)
        {
            if (button_connect.Text == "Connect")
            {
                // If nothing is selected, use default com port
                if (comboBox_availableComPort.SelectedItem != null)
                {
                    try
                    {
                        // Set port name as default port's name
                        serialPort1.PortName = comboBox_availableComPort.SelectedItem.ToString();
                    }
                    catch
                    {

                    }
                }
                else
                {
                    // Set port name as selected port's name
                    serialPort1.PortName = comboBox_availableComPort.Text;
                }

                try
                {
                    if (TestIfParametersAreValid() == Errors.NoErrors)
                    {
                        // Open port
                        serialPort1.Open();

                        // Flush port
                        serialPort1.ReadExisting();

                        // Disable available port list combo box
                        comboBox_availableComPort.Enabled = false;

                        // Change text to "Disconnect"
                        button_connect.Text = "Disconnect";

                        // Enable 'update CLEON time tick' button
                        button_updateCLEON.Enabled = true;

                        // Update status label with opened port information
                        toolStripStatusLabel.Text = serialPort1.PortName.ToString() + " is opened";
                    }
                    else
                    {
                        toolStripStatusLabel.Text = "Invalid parameter exits";
                    }
                }
                catch (Exception Exception)
                {
                    // Pop up message box for error message
                    MessageBox.Show(Exception.Message);
                }
            }
            else if (button_connect.Text == "Disconnect")
            {
                // Close port
                serialPort1.Close();

                // Enable combo box
                comboBox_availableComPort.Enabled = true;

                // Change text to "Connect"
                button_connect.Text = "Connect";

                // Change text of 'CLEON time tick' 
                button_updateCLEON.Text = "Connect to CLEON (1/3)";

                // Disable 'CLEON time tick' button
                button_updateCLEON.Enabled = false;
                textBox_SampleCount.Enabled = true;
                textBox_sampleGap.Enabled = true;
                textBox_ChunkCount.Enabled = true;
                textBox_chunkGap.Enabled = true;
                textBox_batteryCapacity.Enabled = true;

                // Update status label with closed port information
                toolStripStatusLabel.Text = serialPort1.PortName.ToString() + " is closed";
            }
        }

        // If picture 'Help' is clicked
        private void pictureBox1_MouseClick(object sender, EventArgs e)
        {
            Form2 formHelp = new Form2();
            formHelp.Show();
        }

        // Update CLEON's time information
        private void button_updateCLEON_Click(object sender, EventArgs e)
        {
            if (button_updateCLEON.Text == "Connect to CLEON (1/3)")
            {
                if(TestIfParametersAreValid() == Errors.NoErrors)
                {
                    // If port is opened
                    if (serialPort1.IsOpen)
                    {
                        // Send command 'Connect'
                        // (ACK is expected)
                        SendUSBCommand(Constants.commandConnect);

                        TimeSpan maxDuration = TimeSpan.FromMilliseconds(50);
                        Stopwatch sw = Stopwatch.StartNew();

                        // Wait until ACK is received (Duration: 50ms)
                        while ((sw.Elapsed < maxDuration) && !bFLAG_USBFrameReceived) ;

                        // Check if a frame is received
                        if (bFLAG_USBFrameReceived == true)
                        {
                            bFLAG_USBFrameReceived = false;

                            // Check if received frame is ACK
                            if ((USBRecvFrame[3] == Constants.commandAck) && (USBRecvFrame[4] == Constants.commandConnect))
                            {
                                toolStripStatusLabel.Text = "CLEON is connected";
                                textBox_SampleCount.Enabled = false;
                                textBox_sampleGap.Enabled = false;
                                textBox_ChunkCount.Enabled = false;
                                textBox_chunkGap.Enabled = false;
                                textBox_batteryCapacity.Enabled = false;
                                button_updateCLEON.Text = "Update parameters (2/3)";
                            }
                        }
                        else
                        {
                            toolStripStatusLabel.Text = "CLEON doesn't respond";
                        }
                    }
                }
                else
                {
                    toolStripStatusLabel.Text = "Invalid parameter exits";
                }
            }
            else if (button_updateCLEON.Text == "Update parameters (2/3)")
            {
                int iNumberOfParametersSuccessfullyUpdated = 0;

                TimeSpan maxDuration = TimeSpan.FromMilliseconds(50);
                Stopwatch sw;

                if (TestIfParametersAreValid() == Errors.NoErrors)
                {
                    textBox_sampleGap.Enabled = false;

                    // Send command 'Update sample count'
                    // (ACK is expected)
                    SendUSBCommand(Constants.commandUpdateSampleCount);

                    // Start stop watch
                    sw = Stopwatch.StartNew();

                    // Wait until ACK is received (Duration: 50ms)
                    while ((sw.Elapsed < maxDuration) && !bFLAG_USBFrameReceived) ;

                    // Check if a frame is received
                    if (bFLAG_USBFrameReceived == true)
                    {
                        bFLAG_USBFrameReceived = false;

                        // Check if received frame is ACK
                        if ((USBRecvFrame[3] == Constants.commandAck) && (USBRecvFrame[4] == Constants.commandUpdateSampleCount))
                        {
                            toolStripStatusLabel.Text = "Sample count updated";
                            iNumberOfParametersSuccessfullyUpdated++;
                        }
                    }
                    else
                    {
                        toolStripStatusLabel.Text = "CLEON doesn't respond";
                        return;
                    }

                    // Send command 'Update sample gap'
                    // (ACK is expected)
                    SendUSBCommand(Constants.commandUpdateSampleGap);

                    // Start stop watch
                    sw = Stopwatch.StartNew();

                    // Wait until ACK is received (Duration: 50ms)
                    while ((sw.Elapsed < maxDuration) && !bFLAG_USBFrameReceived) ;

                    // Check if a frame is received
                    if (bFLAG_USBFrameReceived == true)
                    {
                        bFLAG_USBFrameReceived = false;

                        // Check if received frame is ACK
                        if ((USBRecvFrame[3] == Constants.commandAck) && (USBRecvFrame[4] == Constants.commandUpdateSampleGap))
                        {
                            toolStripStatusLabel.Text = "Sample gap updated";
                            iNumberOfParametersSuccessfullyUpdated++;
                        }
                    }
                    else
                    {
                        toolStripStatusLabel.Text = "CLEON doesn't respond";
                        return;
                    }

                    // Send command 'Update chunk count'
                    // (ACK is expected)
                    SendUSBCommand(Constants.commandUpdateChunkCount);

                    // Start stop watch
                    sw = Stopwatch.StartNew();

                    // Wait until ACK is received (Duration: 50ms)
                    while ((sw.Elapsed < maxDuration) && !bFLAG_USBFrameReceived) ;

                    // Check if a frame is received
                    if (bFLAG_USBFrameReceived == true)
                    {
                        bFLAG_USBFrameReceived = false;

                        // Check if received frame is ACK
                        if ((USBRecvFrame[3] == Constants.commandAck) && (USBRecvFrame[4] == Constants.commandUpdateChunkCount))
                        {
                            toolStripStatusLabel.Text = "Chunk count updated";
                            iNumberOfParametersSuccessfullyUpdated++;
                        }
                    }
                    else
                    {
                        toolStripStatusLabel.Text = "CLEON doesn't respond";
                        return;
                    }

                    // Send command 'Update chunk gap'
                    // (ACK is expected)
                    SendUSBCommand(Constants.commnadUpdateChunkGap);

                    // Start stop watch
                    sw = Stopwatch.StartNew();

                    // Wait until ACK is received (Duration: 50ms)
                    while ((sw.Elapsed < maxDuration) && !bFLAG_USBFrameReceived) ;

                    // Check if a frame is received
                    if (bFLAG_USBFrameReceived == true)
                    {
                        bFLAG_USBFrameReceived = false;

                        // Check if received frame is ACK
                        if ((USBRecvFrame[3] == Constants.commandAck) && (USBRecvFrame[4] == Constants.commnadUpdateChunkGap))
                        {
                            toolStripStatusLabel.Text = "Chunk gap updated";
                            iNumberOfParametersSuccessfullyUpdated++;
                        }
                    }
                    else
                    {
                        toolStripStatusLabel.Text = "CLEON doesn't respond";
                        return;
                    }

                    if (iNumberOfParametersSuccessfullyUpdated == 4)
                    {
                        toolStripStatusLabel.Text = "User parameters updated (" + iNumberOfParametersSuccessfullyUpdated + "/4)";
                        button_updateCLEON.Text = "Update CLEON time tick (3/3)";
                    }
                    else
                    {
                        toolStripStatusLabel.Text = "User parameter update failed (" + iNumberOfParametersSuccessfullyUpdated + "/4)"; ;
                    }
                }
            }
            else if (button_updateCLEON.Text == "Update CLEON time tick (3/3)")
            {
                // Send command 'Update RTC time'
                // (ACK is expected)
                SendUSBCommand(Constants.commandUpdateRTCTime);

                TimeSpan maxDuration = TimeSpan.FromMilliseconds(50);
                Stopwatch sw1 = Stopwatch.StartNew();

                // Wait until ACK is received (Duration: 50ms)
                while ((sw1.Elapsed < maxDuration) && !bFLAG_USBFrameReceived) ;

                // Check if a frame is received
                if (bFLAG_USBFrameReceived == true)
                {
                    bFLAG_USBFrameReceived = false;

                    // Check if received frame is ACK
                    if ((USBRecvFrame[3] == Constants.commandAck) && (USBRecvFrame[4] == Constants.commandUpdateRTCTime))
                    {
                        toolStripStatusLabel.Text = "RTC time sent successfully";

                        // Send command 'Update Time Tick'
                        // (ACK is expected)
                        SendUSBCommand(Constants.commandUpdateTimeTick);

                        Stopwatch sw2 = Stopwatch.StartNew();

                        // Wait until ACK is received (Duration: 50ms)
                        while ((sw2.Elapsed < maxDuration) && !bFLAG_USBFrameReceived) ;

                        // Check if a frame is received
                        if (bFLAG_USBFrameReceived == true)
                        {
                            bFLAG_USBFrameReceived = false;

                            // Check if received frame is ACK
                            if ((USBRecvFrame[3] == Constants.commandAck) && (USBRecvFrame[4] == Constants.commandUpdateTimeTick))
                            {
                                toolStripStatusLabel.Text = "Sent tick : " + currentTimeTick.ToString();
                                button_updateCLEON.Text = "DONE !!";
                            }
                        }
                        else
                        {
                            toolStripStatusLabel.Text = "CLEON doesn't respond";
                        }
                    }
                }
                else
                {
                    toolStripStatusLabel.Text = "CLEON doesn't respond";
                }
            }
        }

        #region Accquiring text from TextBox
        // Update the value for the 'number of samples' and check if the value is valid
        private void textBox_numberOfSamples_TextChanged(object sender, EventArgs e)
        {
            TestIfParametersAreValid();
        }

        // Update the value for the 'sample gap' and check if the value is valid
        private void textBox_sampleGap_TextChanged(object sender, EventArgs e)
        {
            TestIfParametersAreValid();
        }

        // Update the value for the 'number of chunks' and check if the value is valid
        private void textBox_numberOfChunks_TextChanged(object sender, EventArgs e)
        {
            TestIfParametersAreValid();
        }

        // Update the value for the 'chunk gap' and check if the value is valid
        private void textBox_chunkGap_TextChanged(object sender, EventArgs e)
        {
            TestIfParametersAreValid();
        }

        // Update the value for the 'battery capacity' and check if the value is valid
        private void textBox_batteryCapacity_TextChanged(object sender, EventArgs e)
        {
            TestIfParametersAreValid();
        }
        #endregion
    }
}

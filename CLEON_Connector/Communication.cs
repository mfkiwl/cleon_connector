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
        long currentTimeTick;

        // Called if a byte is received
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            byte receivedByte = 0;

            // If there's some bytes to read
            while (sp.BytesToRead > 0)
            {
                // Read single byte
                receivedByte = (byte)sp.ReadByte();

                // Call received byte processing function
                ProcessReceivedByte(receivedByte);
            }
        }

        // Send a command via USB
        // - There should be an ACK per every command sent
        private void SendUSBCommand(byte command)
        {
            // First two bytes are header (0x55)
            USBSsndFrame[0] = Constants.serialFrameHeader;
            USBSsndFrame[1] = Constants.serialFrameHeader;

            // Byte type array
            byte[] bytes = new byte[4];

            // Insert command to sending frame
            switch (command)
            {
                // Command 'connect (0x01)', 'Update sample count (0x04)', 'Update sample gap (0x05)', 'Update chunk count (0x06)', and 'Update chunk gap (0x07)'
                case Constants.commandConnect:
                    USBSsndFrame[2] = 1;
                    USBSsndFrame[3] = command;
                    break;
                // Command 'Update RTC time' (0x02)
                // - Update year, month, day, hour, minute, and second
                case Constants.commandUpdateRTCTime:
                    DateTime now;
                    int year, month, day, hour, minute, second;
                    // Wait until milisecond becomes zero
                    while (DateTime.Now.Millisecond != 0) ;
                    now = DateTime.Now;
                    year = now.Year;
                    month = now.Month;
                    day = now.Day;
                    hour = now.Hour;
                    minute = now.Minute;
                    second = now.Second;
                    USBSsndFrame[2] = 8;
                    USBSsndFrame[3] = command;
                    USBSsndFrame[4] = (byte)(year >> 8);
                    USBSsndFrame[5] = (byte)(year & 0xff);
                    USBSsndFrame[6] = (byte)month;
                    USBSsndFrame[7] = (byte)day;
                    USBSsndFrame[8] = (byte)hour;
                    USBSsndFrame[9] = (byte)minute;
                    USBSsndFrame[10] = (byte)second;
                    break;
                // Command 'Update time tick' (0x03)
                // - Update UTC time tick
                case Constants.commandUpdateTimeTick:
                    // Wait until millisecond becomes zero
                    while (DateTime.Now.Millisecond != 0) ;
                    currentTimeTick = DateTime.UtcNow.Ticks;
                    // Drop millisecond part because CLEON runs its own clock to calculate millisecond part
                    currentTimeTick = (long)(currentTimeTick / 10000000);
                    currentTimeTick *= 10000000;

                    USBSsndFrame[2] = 9;
                    USBSsndFrame[3] = command;
                    USBSsndFrame[4] = (byte)(currentTimeTick & 0xff);
                    USBSsndFrame[5] = (byte)(currentTimeTick >> 8);
                    USBSsndFrame[6] = (byte)(currentTimeTick >> 16);
                    USBSsndFrame[7] = (byte)(currentTimeTick >> 24);
                    USBSsndFrame[8] = (byte)(currentTimeTick >> 32);
                    USBSsndFrame[9] = (byte)(currentTimeTick >> 40);
                    USBSsndFrame[10] = (byte)(currentTimeTick >> 48);
                    USBSsndFrame[11] = (byte)(currentTimeTick >> 56);
                    break;
                case Constants.commandUpdateSampleCount:
                    bytes = BitConverter.GetBytes(iSampleCount);
                    USBSsndFrame[2] = 5;
                    USBSsndFrame[3] = command;
                    USBSsndFrame[4] = bytes[0];
                    USBSsndFrame[5] = bytes[1];
                    USBSsndFrame[6] = bytes[2];
                    USBSsndFrame[7] = bytes[3];
                    break;
                case Constants.commandUpdateSampleGap:
                    bytes = BitConverter.GetBytes(iSampleGap);
                    USBSsndFrame[2] = 5;
                    USBSsndFrame[3] = command;
                    USBSsndFrame[4] = bytes[0];
                    USBSsndFrame[5] = bytes[1];
                    USBSsndFrame[6] = bytes[2];
                    USBSsndFrame[7] = bytes[3];
                    break;
                case Constants.commandUpdateChunkCount:
                    bytes = BitConverter.GetBytes(iChunkCount);
                    USBSsndFrame[2] = 5;
                    USBSsndFrame[3] = command;
                    USBSsndFrame[4] = bytes[0];
                    USBSsndFrame[5] = bytes[1];
                    USBSsndFrame[6] = bytes[2];
                    USBSsndFrame[7] = bytes[3];
                    break;
                case Constants.commnadUpdateChunkGap:
                    bytes = BitConverter.GetBytes(iChunkGap);
                    USBSsndFrame[2] = 5;
                    USBSsndFrame[3] = command;
                    USBSsndFrame[4] = bytes[0];
                    USBSsndFrame[5] = bytes[1];
                    USBSsndFrame[6] = bytes[2];
                    USBSsndFrame[7] = bytes[3];
                    break;
                default:
                    break;
            }

            // Build CRC
            USBSsndFrame[33] = BuildCRC(USBSsndFrame);

            // Last two bytes are footer (0xAA)
            USBSsndFrame[34] = Constants.serialFrameFooter;
            USBSsndFrame[35] = Constants.serialFrameFooter;

            // Write prepared USBsendFrame
            serialPort1.Write(USBSsndFrame, 0, Constants.serialFrameLength);
        }

        // Build CRC
        private byte BuildCRC(byte[] targetArray)
        {
            byte ucCheckOctet = 0;

            for (int i = 3; i < (Constants.serialFrameLength - 3); i++)
            {
                if (i == 0)
                {
                    ucCheckOctet = targetArray[i];
                }
                if (i >= 1)
                {
                    ucCheckOctet ^= targetArray[i];
                }
            }

            ucCheckOctet = (byte)~ucCheckOctet;

            return ucCheckOctet;
        }

        // State machine for USB frame reception
        private void ProcessReceivedByte(byte receivedByte)
        {
            switch (USBRecvFrameByteCount)
            {
                case 0:
                case 1:
                    if (receivedByte == Constants.serialFrameHeader)
                    {
                        USBRecvFrame[USBRecvFrameByteCount] = receivedByte;
                        USBRecvFrameByteCount++;
                    }
                    else
                    {
                        USBRecvFrameByteCount = 0;
                        // request retransmission
                    }
                    break;
                case 2:
                    if ((USBRecvFrame[0] == Constants.serialFrameHeader) && (USBRecvFrame[1] == Constants.serialFrameHeader) &&
                       (receivedByte > 0) && (receivedByte < Constants.serialFrameDataAndCommandLength))
                    {
                        USBRecvFrame[USBRecvFrameByteCount] = receivedByte;
                        USBRecvFrameByteCount++;
                    }
                    else
                    {
                        USBRecvFrameByteCount = 0;
                        // request retransmission
                    }
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                    USBRecvFrame[USBRecvFrameByteCount] = receivedByte;
                    USBRecvFrameByteCount++;
                    break;
                case 33:
                    if (receivedByte == BuildCRC(USBRecvFrame))
                    {
                        USBRecvFrame[USBRecvFrameByteCount] = receivedByte;
                        USBRecvFrameByteCount++;
                    }
                    else
                    {
                        USBRecvFrameByteCount = 0;
                        // request retransmission
                    }
                    break;
                case 34:
                    if (receivedByte == Constants.serialFrameFooter)
                    {
                        USBRecvFrame[USBRecvFrameByteCount] = receivedByte;
                        USBRecvFrameByteCount++;
                    }
                    else
                    {
                        USBRecvFrameByteCount = 0;
                        // request retransmission
                    }
                    break;
                case 35:
                    if (receivedByte == Constants.serialFrameFooter)
                    {
                        USBRecvFrame[USBRecvFrameByteCount] = receivedByte;
                        USBRecvFrameByteCount = 0;
                        bFLAG_USBFrameReceived = true;
                    }
                    else
                    {
                        USBRecvFrameByteCount = 0;
                        // request retransmission
                    }
                    break;
                default:
                    // request retransmission
                    break;
            }
        }
    }
}
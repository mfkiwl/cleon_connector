using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLEON_Connector
{
    // Public constants
    static class Constants
    {
        // Time span for each operation
        public const int minimumTimespanForCapturingAndStoringAChunk = 50; // 50 ms
        public const int minimumTimespanForGPSStabilization = 100; // 100 ms

        // Power consumption for each operation
        public const double powerConsumptionForGPSinOperation = 30.5; // 30.5 mA
        public const double powerConsumptionForWritingToMicroSD = 40.0; // 40 mA
        public const double powerConsumptionForIdleState = 2.5; // 2.5 mA

        // For serial communication over USB
        public const byte serialFrameLength = 36;

        public const byte serialFrameHeader = 0x55;
        public const byte serialFrameFooter = 0xAA;

        public const byte serialFrameDataAndCommandLength = 30;

        public const byte commandConnect = 0x01;
        public const byte commandUpdateRTCTime = 0x02;
        public const byte commandUpdateTimeTick = 0x03;
        public const byte commandUpdateSampleCount = 0x04;
        public const byte commandUpdateSampleGap = 0x05;
        public const byte commandUpdateChunkCount = 0x06;
        public const byte commnadUpdateChunkGap = 0x07;
        public const byte commandAck = 0xff;
    }
}

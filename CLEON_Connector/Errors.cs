using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLEON_Connector
{
    // Public error numbers
    static class Errors
    {
        public const int NoErrors = 0;
        public const int timespanForASamepleIsTooLong = 1;
        public const int insufficientBatteryCapacity = 2;
        public const int sampleGapIsShorterThanChunkGap = 3;
        public const int invalidValue = 4;
        public const int chunkGapIsTooShort = 5;
        public const int sampleGapIsShorterThanRequiredMinimum = 6;
    }
}

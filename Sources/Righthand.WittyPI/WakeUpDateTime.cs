namespace Righthand.WittyPI
{
    public struct WakeUpDateTime
    {
        public byte Sec;
        public byte? Min;
        public byte? Hour;
        public byte? Day;

        public static WakeUpDateTime Daily(byte hour, byte min, byte sec)
        {
            return new WakeUpDateTime
            {
                Sec = sec,
                Min = min,
                Hour = hour,
                Day = null
            };
        }

        public static WakeUpDateTime Hourly(byte min, byte sec)
        {
            return new WakeUpDateTime
            {
                Sec = sec,
                Min = min,
                Hour = null,
                Day = null
            };
        }

        public static WakeUpDateTime Minutely(byte sec)
        {
            return new WakeUpDateTime
            {
                Sec = sec,
                Min = null,
                Hour = null,
                Day = null
            };
        }
    }
}

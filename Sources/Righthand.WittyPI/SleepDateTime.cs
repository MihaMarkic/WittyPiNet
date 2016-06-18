namespace Righthand.WittyPI
{
    public struct SleepDateTime
    {
        public byte Min;
        public byte Hour;
        public byte Day;

        public SleepDateTime(byte day, byte hour, byte min)
        {
            Day = day;
            Hour = hour;
            Min = min;
        }
    }
}

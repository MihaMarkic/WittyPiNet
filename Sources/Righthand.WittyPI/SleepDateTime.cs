namespace Righthand.WittyPi
{
    /// <summary>
    /// A structure representing sleep date.
    /// </summary>
    public struct SleepDateTime
    {
        /// <summary>
        /// Minute component of the date.
        /// </summary>
        public byte Min;
        /// <summary>
        /// Hour component of the date.
        /// </summary>
        public byte Hour;
        /// <summary>
        /// Day component of the date.
        /// </summary>
        public byte Day;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="min"></param>
        public SleepDateTime(byte day, byte hour, byte min)
        {
            Day = day;
            Hour = hour;
            Min = min;
        }
    }
}

namespace Righthand.WittyPi
{
    /// <summary>
    /// A structure representing wake up date. Null values are used when setting periodical date. Not all null fields combinations are valid, depending on the schedule type.
    /// For createing schedules, use on of the static methods <see cref="Daily"/>, <see cref="Hourly"/> or <see cref="Minutely(byte)"/>.
    /// </summary>
    public struct WakeUpDateTime
    {
        /// <summary>
        /// Second component of the date.
        /// </summary>
        public byte Sec;
        /// <summary>
        /// Minute component of the date.
        /// </summary>
        public byte? Min;
        /// <summary>
        /// Hour component of the date.
        /// </summary>
        public byte? Hour;
        /// <summary>
        /// Day component of the date.
        /// </summary>
        public byte? Day;

        /// <summary>
        /// Creates a daily schedule.
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="min"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates an hourly schedule.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates a minutely schedule.
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
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

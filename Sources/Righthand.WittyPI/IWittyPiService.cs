namespace Righthand.WittyPi
{
    /// <summary>
    /// An interface representing the Witty Pi board.
    /// </summary>
    public interface IWittyPiService
    {
        /// <summary>
        /// Gets or sets wake up date.
        /// </summary>
        WakeUpDateTime WakeUp { get; set; }
        /// <summary>
        /// Gets or sets sleep date.
        /// </summary>
        SleepDateTime Sleep { get; set; }
    }
}
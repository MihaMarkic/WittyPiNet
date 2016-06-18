namespace Righthand.WittyPI
{
    public interface IWittyPiService
    {
        WakeUpDateTime WakeUp { get; set; }
        SleepDateTime Sleep { get; set; }
    }
}
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.InterIntegratedCircuit;

namespace Righthand.WittyPi
{
    /// <summary>
    /// A class representing the Witty Pi board.
    /// </summary>
    public class WittyPiService : IWittyPiService
    {
        private const byte SsdI2cAddress = 0x68;
        private const ConnectorPin SdaPin = ConnectorPin.P1Pin03;
        private const ConnectorPin SclPin = ConnectorPin.P1Pin05;

        /// <summary>
        /// Gets or sets wake up date.
        /// </summary>
        public WakeUpDateTime WakeUp
        {
            get
            {
                using (var i2c = new I2cDriver(SdaPin.ToProcessor(), SclPin.ToProcessor()))
                {
                    var conn = i2c.Connect(SsdI2cAddress);
                    return ReadWakeUpDate(conn);
                }
            }
            set
            {
                using (var i2c = new I2cDriver(SdaPin.ToProcessor(), SclPin.ToProcessor()))
                {
                    var conn = i2c.Connect(SsdI2cAddress);
                    WriteWakeUp(conn, value);
                }
            }
        }
        /// <summary>
        /// Gets or sets sleep date.
        /// </summary>
        public SleepDateTime Sleep
        {
            get
            {
                using (var i2c = new I2cDriver(SdaPin.ToProcessor(), SclPin.ToProcessor()))
                {
                    var conn = i2c.Connect(SsdI2cAddress);
                    return ReadSleepDate(conn);
                }
            }
            set
            {
                using (var i2c = new I2cDriver(SdaPin.ToProcessor(), SclPin.ToProcessor()))
                {
                    var conn = i2c.Connect(SsdI2cAddress);
                    WriteSleep(conn, value);
                }
            }
        }
        /// <summary>
        /// Writes wake up date to board.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="date"></param>
        public static void WriteWakeUp(I2cDeviceConnection conn, WakeUpDateTime date)
        {
            conn.Write(0x07, GetBcd(date.Sec), GetBcd(date.Min), GetBcd(date.Hour), GetBcd(date.Day));
        }
        /// <summary>
        /// Writes sleep date to board.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="date"></param>
        public static void WriteSleep(I2cDeviceConnection conn, SleepDateTime date)
        {
            conn.Write(0x0B, GetBcd(date.Min), GetBcd(date.Hour), GetBcd(date.Day));
        }
        /// <summary>
        /// Reads wake up date from board.
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static WakeUpDateTime ReadWakeUpDate(I2cDeviceConnection conn)
        {
            conn.WriteByte(0x07);
            var bytes = conn.Read(4);
            var piDate = new WakeUpDateTime
            {
                Sec = GetByte(bytes[0]).Value,
                Min = GetByte(bytes[1]),
                Hour = GetByte(bytes[2]),
                Day = GetByte(bytes[3])
            };
            return piDate;
        }
        /// <summary>
        /// Reads sleep date from board.
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static SleepDateTime ReadSleepDate(I2cDeviceConnection conn)
        {
            conn.WriteByte(0x0B);
            var bytes = conn.Read(3);
            var piDate = new SleepDateTime
            {
                Min = GetByte(bytes[0]).Value,
                Hour = GetByte(bytes[1]).Value,
                Day = GetByte(bytes[2]).Value
            };
            return piDate;
        }
        /// <summary>
        /// Transforms byte to BCD.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte GetBcd(byte? value)
        {
            if (value.HasValue)
            {
                return (byte)(value / 10 * 16 + value % 10);
            }
            else
            {
                return 128;
            }
        }
        /// <summary>
        /// Transforms BDC to byte.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static byte? GetByte(byte value)
        {
            if (value == 128)
            {
                return null;
            }
            else
            {
                return (byte)(10 * (value >> 4) + (value & 0xf));
            }
        }
    }
}

using System;
using System.Diagnostics;
using NUnit.Framework;
using Raspberry.IO.InterIntegratedCircuit;

namespace Righthand.WittyPi.Test
{
    public class WittyPiServiceTest
    {
        protected WittyPiService target;

        [SetUp]
        public void SetUp()
        {
            target = new WittyPiService();
        }

        [TestFixture, Pi]
        public class WakeUp: WittyPiServiceTest
        {
            [Test]
            public void WhenValidDate_ReadSameDate()
            {
                var time = new WakeUpDateTime (1, 2, 3, 4 );
                target.WakeUp = time;

                Assert.That(target.WakeUp, Is.EqualTo(time));
            }
        }


        [TestFixture, Pi]
        public class Sleep : WittyPiServiceTest
        {
            [Test]
            public void WhenValidDate_ReadSameDate()
            {
                var time = new SleepDateTime(1, 2, 3);
                target.Sleep = time;

                Assert.That(target.Sleep, Is.EqualTo(time));
            }
        }

        [TestFixture, Pi]
        public class RtcDateTime : WittyPiServiceTest
        {
            [Test]
            public void WhenValidDate_ReadSameDate()
            {
                var time = new DateTime(2016, 6, 27, 17, 41, 15, 20);
                target.RtcDateTime = time;

                var actual = target.RtcDateTime;
                var diff = (actual - time).Seconds;
                Console.WriteLine($"RTC read: {actual}");

                Assert.That(diff, Is.LessThan(5));
                Assert.That(diff, Is.GreaterThanOrEqualTo(0));
            }
        }
    }
}

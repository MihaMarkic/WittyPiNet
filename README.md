# WittyPiNet
.net/Mono library for [WittyPi](http://www.uugear.com/witty-pi-realtime-clock-power-management-for-raspberry-pi/) board.
Currently it supports setting wake up time and sleep time.
## Important

Make sure that WittyPI's RTC clock is synchronized with Raspberry's system clock.
Currently you can run WittyPI's syncTime.sh script or running wittyPi.sh script to manually synchronize it to accomplish it.
Also note that WittyPI's RTC clock might be off system's time widely very soon.

## How to use
```
WittyPiService wittyPi = new WittyPiService();
// wake up hourly at 15 minutes past hour.
wittyPi.WakeUp = WakeUpDateTime.Hourly(15, 00);
// sleep in 2 minutes
var time = DateTime.Now.AddMinutes(2);
wittyPi.Sleep = new SleepDateTime(time.Day, time.Hour, time.Minutes);
```
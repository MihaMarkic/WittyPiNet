# WittyPiNet
.net/Mono library for [Witty Pi](http://www.uugear.com/witty-pi-realtime-clock-power-management-for-raspberry-pi/) board.
It uses a DS1337 circuit, so it should work with that circuit regardless of the (WittyPi) board.
The library supports setting and getting wake up time, sleep time and RTC time.
See also Witty Pi's [manual](http://www.uugear.com/doc/WittyPi_UserManual.pdf).

[![NuGet](https://img.shields.io/nuget/v/WittyPiNet.svg)](https://www.nuget.org/packages/WittyPiNet)

## History

- v1.1.0 RTC time support
- v1.0.0 Sleep and shutdown time support

## How to use
```
IWittyPiService wittyPi = new WittyPiService();
// wake up hourly at 15 minutes past hour.
wittyPi.WakeUp = WakeUpDateTime.Hourly(15, 00);
// sleep in 2 minutes
var time = DateTime.Now.AddMinutes(2);
wittyPi.Sleep = new SleepDateTime(time.Day, time.Hour, time.Minutes);
```

[![Follow @mihamarkic](https://img.shields.io/badge/Twitter-Follow%20%40mihamarkic-blue.svg)](https://twitter.com/intent/follow?screen_name=mihamarkic)
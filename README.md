# Project Lambo

**Project Lambo** is being developed to buy a lambo for an initial amount of 100$ with coin trading in crypto exchanges.

Two exchanges are supported at the current time. API endpoints have been implemented for further usage. There is neither signals nor strategies are developed yet.

## Installation

Clone the repository, build it and reference in your projects.

## Exchanges

Supported exchanges:
* [Bitmex](https://www.bitmex.com/register/SZGMWx)
* [Okex](https://www.okex.com/join/1913670) (Both token and swap trading options)

*Todo: Add more details about supported features and exchanges here.*

An example code snippet for trading in Bitmex is given below;

```
var api = new BitmexAPI("my_api_key", "my_api_secret");
var ticker = api.GetTicker("XBTUSD");
if (ticker.LastPrice < 5000)
    var orderID = exchange.NewOrder("XBTUSD", OrderType.Market, 100, OrderSide.Buy);
```
## Last Words

To support this project;

BTC: *1KTbqnBcdmRzemgpATifZGKZ2nXkUqAtEn*

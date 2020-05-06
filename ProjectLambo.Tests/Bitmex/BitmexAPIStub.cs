using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectLambo.Bitmex;

namespace ProjectLambo.Tests.Bitmex
{
    internal class BitmexAPIStub : BitmexWebRequest
    {
        #region Response Strings

        private const string s_get_user =
            @"{
              ""id"": 123456789,
              ""ownerId"": 0,
              ""firstname"": ""satoshi"",
              ""lastname"": ""nakamoto"",
              ""username"": ""satoshinakamoto"",
              ""email"": ""satoshi@bitcoin.org"",
              ""phone"": ""string"",
              ""created"": ""2019-09-11T15:13:09.907Z"",
              ""lastUpdated"": ""2019-09-11T15:13:09.907Z"",
              ""preferences"": {
                ""alertOnLiquidations"": true,
                ""animationsEnabled"": true,
                ""announcementsLastSeen"": ""2019-09-11T15:13:09.915Z"",
                ""chatChannelID"": 0,
                ""colorTheme"": ""string"",
                ""currency"": ""string"",
                ""debug"": true,
                ""disableEmails"": [
                  ""string""
                ],
                ""disablePush"": [
                  ""string""
                ],
                ""hideConfirmDialogs"": [
                  ""string""
                ],
                ""hideConnectionModal"": true,
                ""hideFromLeaderboard"": false,
                ""hideNameFromLeaderboard"": true,
                ""hideNotifications"": [
                  ""string""
                ],
                ""locale"": ""en-US"",
                ""msgsSeen"": [
                  ""string""
                ],
                ""orderBookBinning"": {},
                ""orderBookType"": ""string"",
                ""orderClearImmediate"": false,
                ""orderControlsPlusMinus"": true,
                ""showLocaleNumbers"": true,
                ""sounds"": [
                  ""string""
                ],
                ""strictIPCheck"": false,
                ""strictTimeout"": true,
                ""tickerGroup"": ""string"",
                ""tickerPinned"": true,
                ""tradeLayout"": ""string""
              },
              ""restrictedEngineFields"": {},
              ""TFAEnabled"": ""string"",
              ""affiliateID"": ""string"",
              ""pgpPubKey"": ""string"",
              ""country"": ""string"",
              ""geoipCountry"": ""string"",
              ""geoipRegion"": ""string"",
              ""typ"": ""string""
            }";

        private const string s_get_wallet =
            @"{
              ""account"": 0,
              ""currency"": ""XBt"",
              ""prevDeposited"": 0,
              ""prevWithdrawn"": 0,
              ""prevTransferIn"": 0,
              ""prevTransferOut"": 0,
              ""prevAmount"": 0,
              ""prevTimestamp"": ""2019-09-11T15:13:10.010Z"",
              ""deltaDeposited"": 0,
              ""deltaWithdrawn"": 0,
              ""deltaTransferIn"": 0,
              ""deltaTransferOut"": 0,
              ""deltaAmount"": 0,
              ""deposited"": 0,
              ""withdrawn"": 0,
              ""transferIn"": 0,
              ""transferOut"": 0,
              ""amount"": 10000000,
              ""pendingCredit"": 0,
              ""pendingDebit"": 0,
              ""confirmedDebit"": 0,
              ""timestamp"": ""2019-09-11T15:13:10.010Z"",
              ""addr"": ""string"",
              ""script"": ""string"",
              ""withdrawalLock"": [
                ""string""
              ]
            }";

        private const string s_get_margin =
            @"{
              ""account"": 0,
              ""currency"": ""XBt"",
              ""riskLimit"": 0,
              ""prevState"": ""string"",
              ""state"": ""string"",
              ""action"": ""string"",
              ""amount"": 0,
              ""pendingCredit"": 0,
              ""pendingDebit"": 0,
              ""confirmedDebit"": 0,
              ""prevRealisedPnl"": 0,
              ""prevUnrealisedPnl"": 0,
              ""grossComm"": 0,
              ""grossOpenCost"": 0,
              ""grossOpenPremium"": 0,
              ""grossExecCost"": 0,
              ""grossMarkValue"": 0,
              ""riskValue"": 0,
              ""taxableMargin"": 0,
              ""initMargin"": 0,
              ""maintMargin"": 0,
              ""sessionMargin"": 0,
              ""targetExcessMargin"": 0,
              ""varMargin"": 0,
              ""realisedPnl"": 0,
              ""unrealisedPnl"": 0,
              ""indicativeTax"": 0,
              ""unrealisedProfit"": 0,
              ""syntheticMargin"": 0,
              ""walletBalance"": 0,
              ""marginBalance"": 10000000,
              ""marginBalancePcnt"": 0,
              ""marginLeverage"": 0,
              ""marginUsedPcnt"": 0,
              ""excessMargin"": 0,
              ""excessMarginPcnt"": 0,
              ""availableMargin"": 10000000,
              ""withdrawableMargin"": 0,
              ""timestamp"": ""2019-09-11T15:13:09.978Z"",
              ""grossLastValue"": 0,
              ""commission"": 0
            }";

        private const string s_get_position =
            @"[
              {
                ""account"": 0,
                ""symbol"": ""XBTUSD"",
                ""currency"": ""string"",
                ""underlying"": ""string"",
                ""quoteCurrency"": ""string"",
                ""commission"": 0,
                ""initMarginReq"": 0,
                ""maintMarginReq"": 0,
                ""riskLimit"": 0,
                ""leverage"": 1,
                ""crossMargin"": true,
                ""deleveragePercentile"": 0,
                ""rebalancedPnl"": 0,
                ""prevRealisedPnl"": 0,
                ""prevUnrealisedPnl"": 0,
                ""prevClosePrice"": 0,
                ""openingTimestamp"": ""2019-09-11T15:13:09.629Z"",
                ""openingQty"": 0,
                ""openingCost"": 0,
                ""openingComm"": 0,
                ""openOrderBuyQty"": 0,
                ""openOrderBuyCost"": 0,
                ""openOrderBuyPremium"": 0,
                ""openOrderSellQty"": 0,
                ""openOrderSellCost"": 0,
                ""openOrderSellPremium"": 0,
                ""execBuyQty"": 0,
                ""execBuyCost"": 0,
                ""execSellQty"": 0,
                ""execSellCost"": 0,
                ""execQty"": 0,
                ""execCost"": 0,
                ""execComm"": 0,
                ""currentTimestamp"": ""2019-09-11T15:13:09.629Z"",
                ""currentQty"": 10000000,
                ""currentCost"": 0,
                ""currentComm"": 0,
                ""realisedCost"": 0,
                ""unrealisedCost"": 0,
                ""grossOpenCost"": 0,
                ""grossOpenPremium"": 0,
                ""grossExecCost"": 0,
                ""isOpen"": true,
                ""markPrice"": 0,
                ""markValue"": 0,
                ""riskValue"": 0,
                ""homeNotional"": 0,
                ""foreignNotional"": 0,
                ""posState"": ""string"",
                ""posCost"": 0,
                ""posCost2"": 0,
                ""posCross"": 0,
                ""posInit"": 0,
                ""posComm"": 0,
                ""posLoss"": 0,
                ""posMargin"": 0,
                ""posMaint"": 0,
                ""posAllowance"": 0,
                ""taxableMargin"": 0,
                ""initMargin"": 0,
                ""maintMargin"": 0,
                ""sessionMargin"": 0,
                ""targetExcessMargin"": 0,
                ""varMargin"": 0,
                ""realisedGrossPnl"": 0,
                ""realisedTax"": 0,
                ""realisedPnl"": 0,
                ""unrealisedGrossPnl"": 0,
                ""longBankrupt"": 0,
                ""shortBankrupt"": 0,
                ""taxBase"": 0,
                ""indicativeTaxRate"": 0,
                ""indicativeTax"": 0,
                ""unrealisedTax"": 0,
                ""unrealisedPnl"": 100000,
                ""unrealisedPnlPcnt"": 0,
                ""unrealisedRoePcnt"": 0,
                ""simpleQty"": 0,
                ""simpleCost"": 0,
                ""simpleValue"": 0,
                ""simplePnl"": 0,
                ""simplePnlPcnt"": 0,
                ""avgCostPrice"": 0,
                ""avgEntryPrice"": 9990,
                ""breakEvenPrice"": 0,
                ""marginCallPrice"": 0,
                ""liquidationPrice"": 8880,
                ""bankruptPrice"": 0,
                ""timestamp"": ""2019-09-11T15:13:09.629Z"",
                ""lastPrice"": 9990,
                ""lastValue"": 0
              }
            ]";

        private const string s_get_ticker =
            @"[
              {
                ""symbol"": ""XBTUSD"",
                ""rootSymbol"": ""XBT"",
                ""state"": ""Open"",
                ""typ"": ""FFWCSX"",
                ""listing"": ""2016-05-13T12:00:00.000Z"",
                ""front"": ""2016-05-13T12:00:00.000Z"",
                ""expiry"": null,
                ""settle"": null,
                ""relistInterval"": null,
                ""inverseLeg"": """",
                ""sellLeg"": """",
                ""buyLeg"": """",
                ""optionStrikePcnt"": null,
                ""optionStrikeRound"": null,
                ""optionStrikePrice"": null,
                ""optionMultiplier"": null,
                ""positionCurrency"": ""USD"",
                ""underlying"": ""XBT"",
                ""quoteCurrency"": ""USD"",
                ""underlyingSymbol"": ""XBT="",
                ""reference"": ""BMEX"",
                ""referenceSymbol"": "".BXBT"",
                ""calcInterval"": null,
                ""publishInterval"": null,
                ""publishTime"": null,
                ""maxOrderQty"": 10000000,
                ""maxPrice"": 1000000,
                ""lotSize"": 1,
                ""tickSize"": 0.5,
                ""multiplier"": -100000000,
                ""settlCurrency"": ""XBt"",
                ""underlyingToPositionMultiplier"": null,
                ""underlyingToSettleMultiplier"": -100000000,
                ""quoteToSettleMultiplier"": null,
                ""isQuanto"": false,
                ""isInverse"": true,
                ""initMargin"": 0.01,
                ""maintMargin"": 0.005,
                ""riskLimit"": 20000000000,
                ""riskStep"": 10000000000,
                ""limit"": null,
                ""capped"": false,
                ""taxed"": true,
                ""deleverage"": true,
                ""makerFee"": -0.00025,
                ""takerFee"": 0.00075,
                ""settlementFee"": 0,
                ""insuranceFee"": 0,
                ""fundingBaseSymbol"": "".XBTBON8H"",
                ""fundingQuoteSymbol"": "".USDBON8H"",
                ""fundingPremiumSymbol"": "".XBTUSDPI8H"",
                ""fundingTimestamp"": ""2019-09-11T20:00:00.000Z"",
                ""fundingInterval"": ""2000-01-01T08:00:00.000Z"",
                ""fundingRate"": 0.0001,
                ""indicativeFundingRate"": 0.0001,
                ""rebalanceTimestamp"": null,
                ""rebalanceInterval"": null,
                ""openingTimestamp"": ""2019-09-11T15:00:00.000Z"",
                ""closingTimestamp"": ""2019-09-11T16:00:00.000Z"",
                ""sessionInterval"": ""2000-01-01T01:00:00.000Z"",
                ""prevClosePrice"": 10055.4,
                ""limitDownPrice"": null,
                ""limitUpPrice"": null,
                ""bankruptLimitDownPrice"": null,
                ""bankruptLimitUpPrice"": null,
                ""prevTotalVolume"": 1669305460795,
                ""totalVolume"": 1669369757810,
                ""volume"": 64297015,
                ""volume24h"": 3094565223,
                ""prevTotalTurnover"": 23962501134168216,
                ""totalTurnover"": 23963143975494436,
                ""turnover"": 642841326218,
                ""turnover24h"": 30780467772722,
                ""homeNotional24h"": 307804.67772722145,
                ""foreignNotional24h"": 3094565223,
                ""prevPrice24h"": 10193.5,
                ""vwap"": 10054.2932,
                ""highPrice"": 10280,
                ""lowPrice"": 9850,
                ""lastPrice"": 10007.5,
                ""lastPriceProtected"": 10007.5,
                ""lastTickDirection"": ""ZeroMinusTick"",
                ""lastChangePcnt"": -0.0182,
                ""bidPrice"": 10007.5,
                ""midPrice"": 10007.75,
                ""askPrice"": 10008,
                ""impactBidPrice"": 10007.0049,
                ""impactMidPrice"": 10007.5,
                ""impactAskPrice"": 10008.0064,
                ""hasLiquidity"": true,
                ""openInterest"": 936208320,
                ""openValue"": 9360210783360,
                ""fairMethod"": ""FundingRate"",
                ""fairBasisRate"": 0.1095,
                ""fairBasis"": 0.54,
                ""fairPrice"": 10002.12,
                ""markMethod"": ""FairPrice"",
                ""markPrice"": 10002.12,
                ""indicativeTaxRate"": 0,
                ""indicativeSettlePrice"": 10001.58,
                ""optionUnderlyingPrice"": null,
                ""settledPrice"": null,
                ""timestamp"": ""2019-09-11T15:40:42.100Z""
              }
            ]";

        private const string s_get_order_with_orderid =
            @"[
              {
                ""orderID"": ""some_order_id"",
                ""clOrdID"": ""string"",
                ""clOrdLinkID"": ""string"",
                ""account"": 0,
                ""symbol"": ""XBTUSD"",
                ""side"": ""Buy"",
                ""simpleOrderQty"": 0,
                ""orderQty"": 10000000,
                ""price"": 9350,
                ""displayQty"": 0,
                ""stopPx"": 0,
                ""pegOffsetValue"": 0,
                ""pegPriceType"": ""string"",
                ""currency"": ""string"",
                ""settlCurrency"": ""string"",
                ""ordType"": ""Limit"",
                ""timeInForce"": ""string"",
                ""execInst"": ""string"",
                ""contingencyType"": ""string"",
                ""exDestination"": ""string"",
                ""ordStatus"": ""New"",
                ""triggered"": ""string"",
                ""workingIndicator"": true,
                ""ordRejReason"": ""string"",
                ""simpleLeavesQty"": 0,
                ""leavesQty"": 10000000,
                ""simpleCumQty"": 0,
                ""cumQty"": 0,
                ""avgPx"": 0,
                ""multiLegReportingType"": ""string"",
                ""text"": ""string"",
                ""transactTime"": ""2019-09-11T15:13:09.445Z"",
                ""timestamp"": ""2019-09-11T15:13:09.445Z""
              }
            ]";

        private const string s_get_open_orders =
            @"[
              {
                ""orderID"": ""some_order_id"",
                ""clOrdID"": ""string"",
                ""clOrdLinkID"": ""string"",
                ""account"": 0,
                ""symbol"": ""XBTUSD"",
                ""side"": ""Buy"",
                ""simpleOrderQty"": 0,
                ""orderQty"": 10000000,
                ""price"": 9350,
                ""displayQty"": 0,
                ""stopPx"": 0,
                ""pegOffsetValue"": 0,
                ""pegPriceType"": ""string"",
                ""currency"": ""string"",
                ""settlCurrency"": ""string"",
                ""ordType"": ""Limit"",
                ""timeInForce"": ""string"",
                ""execInst"": ""string"",
                ""contingencyType"": ""string"",
                ""exDestination"": ""string"",
                ""ordStatus"": ""New"",
                ""triggered"": ""string"",
                ""workingIndicator"": true,
                ""ordRejReason"": ""string"",
                ""simpleLeavesQty"": 0,
                ""leavesQty"": 10000000,
                ""simpleCumQty"": 0,
                ""cumQty"": 0,
                ""avgPx"": 0,
                ""multiLegReportingType"": ""string"",
                ""text"": ""string"",
                ""transactTime"": ""2019-09-11T15:13:09.445Z"",
                ""timestamp"": ""2019-09-11T15:13:09.445Z""
              },
              {
                ""orderID"": ""some_order_id"",
                ""clOrdID"": ""string"",
                ""clOrdLinkID"": ""string"",
                ""account"": 0,
                ""symbol"": ""XBTUSD"",
                ""side"": ""Sell"",
                ""simpleOrderQty"": 0,
                ""orderQty"": 10000000,
                ""price"": 9350,
                ""displayQty"": 0,
                ""stopPx"": 9300,
                ""pegOffsetValue"": 0,
                ""pegPriceType"": ""string"",
                ""currency"": ""string"",
                ""settlCurrency"": ""string"",
                ""ordType"": ""StopLimit"",
                ""timeInForce"": ""string"",
                ""execInst"": ""string"",
                ""contingencyType"": ""string"",
                ""exDestination"": ""string"",
                ""ordStatus"": ""New"",
                ""triggered"": ""string"",
                ""workingIndicator"": true,
                ""ordRejReason"": ""string"",
                ""simpleLeavesQty"": 0,
                ""leavesQty"": 10000000,
                ""simpleCumQty"": 0,
                ""cumQty"": 0,
                ""avgPx"": 0,
                ""multiLegReportingType"": ""string"",
                ""text"": ""string"",
                ""transactTime"": ""2019-09-11T15:13:09.445Z"",
                ""timestamp"": ""2019-09-11T15:13:09.445Z""
              },
              {
                ""orderID"": ""some_order_id"",
                ""clOrdID"": ""string"",
                ""clOrdLinkID"": ""string"",
                ""account"": 0,
                ""symbol"": ""XBTUSD"",
                ""side"": ""Sell"",
                ""simpleOrderQty"": 0,
                ""orderQty"": 10000000,
                ""price"": 9850,
                ""displayQty"": 0,
                ""stopPx"": 0,
                ""pegOffsetValue"": 0,
                ""pegPriceType"": ""string"",
                ""currency"": ""string"",
                ""settlCurrency"": ""string"",
                ""ordType"": ""Limit"",
                ""timeInForce"": ""string"",
                ""execInst"": ""Close"",
                ""contingencyType"": ""string"",
                ""exDestination"": ""string"",
                ""ordStatus"": ""New"",
                ""triggered"": ""string"",
                ""workingIndicator"": true,
                ""ordRejReason"": ""string"",
                ""simpleLeavesQty"": 0,
                ""leavesQty"": 10000000,
                ""simpleCumQty"": 0,
                ""cumQty"": 0,
                ""avgPx"": 0,
                ""multiLegReportingType"": ""string"",
                ""text"": ""string"",
                ""transactTime"": ""2019-09-11T15:13:09.445Z"",
                ""timestamp"": ""2019-09-11T15:13:09.445Z""
              }
            ]";

        private const string s_get_orderbook =
            @"[
              {
                ""symbol"": ""XBTUSD"",
                ""id"": 8798967750,
                ""side"": ""Sell"",
                ""size"": 157531,
                ""price"": 10322.5
              },
              {
                ""symbol"": ""XBTUSD"",
                ""id"": 8798967800,
                ""side"": ""Sell"",
                ""size"": 87873,
                ""price"": 10322
              },
              {
                ""symbol"": ""XBTUSD"",
                ""id"": 8798967850,
                ""side"": ""Sell"",
                ""size"": 399910,
                ""price"": 10321.5
              },
              {
                ""symbol"": ""XBTUSD"",
                ""id"": 8798967900,
                ""side"": ""Sell"",
                ""size"": 128786,
                ""price"": 10321
              },
              {
                ""symbol"": ""XBTUSD"",
                ""id"": 8798967950,
                ""side"": ""Sell"",
                ""size"": 62858,
                ""price"": 10320.5
              },
            ]";

        private const string s_get_candles =
            @"[
              {
                ""timestamp"": ""2017-01-01T00:00:00.000Z"",
                ""symbol"": ""XBTUSD"",
                ""open"": 968.29,
                ""high"": 968.29,
                ""low"": 968.29,
                ""close"": 968.29,
                ""trades"": 0,
                ""volume"": 0,
                ""vwap"": null,
                ""lastSize"": null,
                ""turnover"": 0,
                ""homeNotional"": 0,
                ""foreignNotional"": 0
              },
              {
                ""timestamp"": ""2017-01-01T00:01:00.000Z"",
                ""symbol"": ""XBTUSD"",
                ""open"": 968.29,
                ""high"": 968.76,
                ""low"": 968.49,
                ""close"": 968.7,
                ""trades"": 17,
                ""volume"": 12993,
                ""vwap"": 968.72,
                ""lastSize"": 2000,
                ""turnover"": 1341256747,
                ""homeNotional"": 13.412567469999997,
                ""foreignNotional"": 12993
              },
              {
                ""timestamp"": ""2017-01-01T00:02:00.000Z"",
                ""symbol"": ""XBTUSD"",
                ""open"": 968.7,
                ""high"": 968.7,
                ""low"": 967.2,
                ""close"": 968.43,
                ""trades"": 19,
                ""volume"": 73800,
                ""vwap"": 967.7638,
                ""lastSize"": 2000,
                ""turnover"": 7625860502,
                ""homeNotional"": 76.25860502,
                ""foreignNotional"": 73800
              },
              {
                ""timestamp"": ""2017-01-01T00:03:00.000Z"",
                ""symbol"": ""XBTUSD"",
                ""open"": 968.43,
                ""high"": 968,
                ""low"": 967.21,
                ""close"": 967.21,
                ""trades"": 4,
                ""volume"": 3500,
                ""vwap"": 967.32,
                ""lastSize"": 1000,
                ""turnover"": 361823000,
                ""homeNotional"": 3.61823,
                ""foreignNotional"": 3500
              },
              {
                ""timestamp"": ""2017-01-01T00:04:00.000Z"",
                ""symbol"": ""XBTUSD"",
                ""open"": 967.21,
                ""high"": 967.21,
                ""low"": 966.74,
                ""close"": 966.97,
                ""trades"": 17,
                ""volume"": 15969,
                ""vwap"": 967.0806,
                ""lastSize"": 100,
                ""turnover"": 1651262097,
                ""homeNotional"": 16.512620969999997,
                ""foreignNotional"": 15969
              }
            ]";

        private const string s_new_order_close_limit =
            @"{
              ""orderID"": ""169da0fe-883c-a8f0-a545-8ac430f25bc4"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Buy"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": 11000,
              ""displayQty"": null,
              ""stopPx"": null,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Limit"",
              ""timeInForce"": ""GoodTillCancel"",
              ""execInst"": ""Close"",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""Canceled"",
              ""triggered"": """",
              ""workingIndicator"": false,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 0,
              ""simpleCumQty"": null,
              ""cumQty"": 0,
              ""avgPx"": null,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Canceled: Order had execInst of Close or ReduceOnly and side of Buy but current position is 0\nSubmitted via API."",
              ""transactTime"": ""2019-09-15T19:57:08.048Z"",
              ""timestamp"": ""2019-09-15T19:57:08.048Z""
            }";

        private const string s_new_order_limit_buy =
            @"{
              ""orderID"": ""909d18d1-b2e1-5b44-e65e-46afddc9456a"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Buy"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": 9000,
              ""displayQty"": null,
              ""stopPx"": null,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Limit"",
              ""timeInForce"": ""GoodTillCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""New"",
              ""triggered"": """",
              ""workingIndicator"": true,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 100,
              ""simpleCumQty"": null,
              ""cumQty"": 0,
              ""avgPx"": null,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T19:54:41.938Z"",
              ""timestamp"": ""2019-09-15T19:54:41.938Z""
            }";

        private const string s_new_order_limit_sell =
            @"{
              ""orderID"": ""c16e6e2e-a790-b202-9bfc-33552a7a70a3"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Sell"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": 11000,
              ""displayQty"": null,
              ""stopPx"": null,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Limit"",
              ""timeInForce"": ""GoodTillCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""New"",
              ""triggered"": """",
              ""workingIndicator"": true,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 100,
              ""simpleCumQty"": null,
              ""cumQty"": 0,
              ""avgPx"": null,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T19:55:48.229Z"",
              ""timestamp"": ""2019-09-15T19:55:48.229Z""
            }";

        private const string s_new_order_close_market =
            @"{
              ""orderID"": ""0fc5daba-addd-f583-2fca-e4589245f4ff"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Buy"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": 10305.5,
              ""displayQty"": null,
              ""stopPx"": null,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Market"",
              ""timeInForce"": ""ImmediateOrCancel"",
              ""execInst"": ""Close"",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""Filled"",
              ""triggered"": """",
              ""workingIndicator"": false,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 0,
              ""simpleCumQty"": null,
              ""cumQty"": 100,
              ""avgPx"": 10305,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T20:26:59.729Z"",
              ""timestamp"": ""2019-09-15T20:26:59.729Z""
            }";

        private const string s_new_order_market_buy =
            @"{
              ""orderID"": ""151a0c30-47f3-93fb-5977-65faa1e45611"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Buy"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": 10306.5,
              ""displayQty"": null,
              ""stopPx"": null,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Market"",
              ""timeInForce"": ""ImmediateOrCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""Filled"",
              ""triggered"": """",
              ""workingIndicator"": false,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 0,
              ""simpleCumQty"": null,
              ""cumQty"": 100,
              ""avgPx"": 10306,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T20:25:02.077Z"",
              ""timestamp"": ""2019-09-15T20:25:02.077Z""
            }";

        private const string s_new_order_market_sell =
            @"{
              ""orderID"": ""7486a283-14ce-4d5b-2d03-d60caa8447d7"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Sell"",
              ""simpleOrderQty"": null,
              ""orderQty"": 200,
              ""price"": 10305.5,
              ""displayQty"": null,
              ""stopPx"": null,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Market"",
              ""timeInForce"": ""ImmediateOrCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""Filled"",
              ""triggered"": """",
              ""workingIndicator"": false,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 0,
              ""simpleCumQty"": null,
              ""cumQty"": 200,
              ""avgPx"": 10305,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T20:25:42.955Z"",
              ""timestamp"": ""2019-09-15T20:25:42.955Z""
            }";

        private const string s_new_order_stop_limit_buy =
            @"{
              ""orderID"": ""3e9353ce-2d2b-6253-81f4-95c72bba26db"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Buy"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": 9000,
              ""displayQty"": null,
              ""stopPx"": 9100,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""StopLimit"",
              ""timeInForce"": ""GoodTillCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""New"",
              ""triggered"": """",
              ""workingIndicator"": false,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 100,
              ""simpleCumQty"": null,
              ""cumQty"": 0,
              ""avgPx"": null,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T20:28:36.392Z"",
              ""timestamp"": ""2019-09-15T20:28:36.392Z""
            }";

        private const string s_new_order_stop_limit_sell =
            @"{
              ""orderID"": ""51c0437e-ac41-f8c8-92c2-f134a9cb37b8"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Sell"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": 9000,
              ""displayQty"": null,
              ""stopPx"": 9100,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""StopLimit"",
              ""timeInForce"": ""GoodTillCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""New"",
              ""triggered"": """",
              ""workingIndicator"": false,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 100,
              ""simpleCumQty"": null,
              ""cumQty"": 0,
              ""avgPx"": null,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T20:29:35.300Z"",
              ""timestamp"": ""2019-09-15T20:29:35.300Z""
            }";

        private const string s_new_order_stop_market_buy =
            @"{
              ""orderID"": ""a5fe26f8-13f0-6562-e07d-68b7f0d729f1"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Buy"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": null,
              ""displayQty"": null,
              ""stopPx"": 11000,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Stop"",
              ""timeInForce"": ""ImmediateOrCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""New"",
              ""triggered"": """",
              ""workingIndicator"": false,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 100,
              ""simpleCumQty"": null,
              ""cumQty"": 0,
              ""avgPx"": null,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T20:30:51.103Z"",
              ""timestamp"": ""2019-09-15T20:30:51.103Z""
            }";

        private const string s_new_order_stop_market_sell =
            @"{
              ""orderID"": ""1d3aefa8-6b4e-ac15-66d3-d8026909d2b6"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Sell"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": null,
              ""displayQty"": null,
              ""stopPx"": 9000,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Stop"",
              ""timeInForce"": ""ImmediateOrCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""New"",
              ""triggered"": """",
              ""workingIndicator"": false,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 100,
              ""simpleCumQty"": null,
              ""cumQty"": 0,
              ""avgPx"": null,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T20:31:19.546Z"",
              ""timestamp"": ""2019-09-15T20:31:19.546Z""
            }";

        private const string s_amend_order =
            @"{
              ""orderID"": ""3e5246da-73b9-8123-867c-0a9bcfc0214b"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Buy"",
              ""simpleOrderQty"": null,
              ""orderQty"": 200,
              ""price"": 9200,
              ""displayQty"": null,
              ""stopPx"": null,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Limit"",
              ""timeInForce"": ""GoodTillCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""New"",
              ""triggered"": """",
              ""workingIndicator"": true,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 200,
              ""simpleCumQty"": null,
              ""cumQty"": 0,
              ""avgPx"": null,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Amended orderQty price: Amended via API.\nSubmitted via API."",
              ""transactTime"": ""2019-09-15T20:53:38.066Z"",
              ""timestamp"": ""2019-09-15T20:53:38.066Z""
            }";

        private const string s_delete_order =
            @"[
              {
                ""orderID"": ""3e5246da-73b9-8123-867c-0a9bcfc0214b"",
                ""clOrdID"": """",
                ""clOrdLinkID"": """",
                ""account"": 661349,
                ""symbol"": ""XBTUSD"",
                ""side"": ""Buy"",
                ""simpleOrderQty"": null,
                ""orderQty"": 200,
                ""price"": 9200,
                ""displayQty"": null,
                ""stopPx"": null,
                ""pegOffsetValue"": null,
                ""pegPriceType"": """",
                ""currency"": ""USD"",
                ""settlCurrency"": ""XBt"",
                ""ordType"": ""Limit"",
                ""timeInForce"": ""GoodTillCancel"",
                ""execInst"": """",
                ""contingencyType"": """",
                ""exDestination"": ""XBME"",
                ""ordStatus"": ""Canceled"",
                ""triggered"": """",
                ""workingIndicator"": false,
                ""ordRejReason"": """",
                ""simpleLeavesQty"": null,
                ""leavesQty"": 0,
                ""simpleCumQty"": null,
                ""cumQty"": 0,
                ""avgPx"": null,
                ""multiLegReportingType"": ""SingleSecurity"",
                ""text"": ""Canceled: Canceled via API.\nSubmitted via API."",
                ""transactTime"": ""2019-09-15T20:53:38.066Z"",
                ""timestamp"": ""2019-09-15T20:55:15.786Z""
              }
            ]";

        private const string s_delete_all_orders =
            @"[
              {
                ""orderID"": ""3e5246da-73b9-8123-867c-0a9bcfc0214b"",
                ""clOrdID"": """",
                ""clOrdLinkID"": """",
                ""account"": 661349,
                ""symbol"": ""XBTUSD"",
                ""side"": ""Buy"",
                ""simpleOrderQty"": null,
                ""orderQty"": 200,
                ""price"": 9200,
                ""displayQty"": null,
                ""stopPx"": null,
                ""pegOffsetValue"": null,
                ""pegPriceType"": """",
                ""currency"": ""USD"",
                ""settlCurrency"": ""XBt"",
                ""ordType"": ""Limit"",
                ""timeInForce"": ""GoodTillCancel"",
                ""execInst"": """",
                ""contingencyType"": """",
                ""exDestination"": ""XBME"",
                ""ordStatus"": ""Canceled"",
                ""triggered"": """",
                ""workingIndicator"": false,
                ""ordRejReason"": """",
                ""simpleLeavesQty"": null,
                ""leavesQty"": 0,
                ""simpleCumQty"": null,
                ""cumQty"": 0,
                ""avgPx"": null,
                ""multiLegReportingType"": ""SingleSecurity"",
                ""text"": ""Canceled: Canceled via API.\nSubmitted via API."",
                ""transactTime"": ""2019-09-15T20:53:38.066Z"",
                ""timestamp"": ""2019-09-15T20:55:15.786Z""
              },
              {
              ""orderID"": ""1d3aefa8-6b4e-ac15-66d3-d8026909d2b6"",
              ""clOrdID"": """",
              ""clOrdLinkID"": """",
              ""account"": 661349,
              ""symbol"": ""XBTUSD"",
              ""side"": ""Sell"",
              ""simpleOrderQty"": null,
              ""orderQty"": 100,
              ""price"": null,
              ""displayQty"": null,
              ""stopPx"": 9000,
              ""pegOffsetValue"": null,
              ""pegPriceType"": """",
              ""currency"": ""USD"",
              ""settlCurrency"": ""XBt"",
              ""ordType"": ""Stop"",
              ""timeInForce"": ""ImmediateOrCancel"",
              ""execInst"": """",
              ""contingencyType"": """",
              ""exDestination"": ""XBME"",
              ""ordStatus"": ""New"",
              ""triggered"": """",
              ""workingIndicator"": false,
              ""ordRejReason"": """",
              ""simpleLeavesQty"": null,
              ""leavesQty"": 100,
              ""simpleCumQty"": null,
              ""cumQty"": 0,
              ""avgPx"": null,
              ""multiLegReportingType"": ""SingleSecurity"",
              ""text"": ""Submitted via API."",
              ""transactTime"": ""2019-09-15T20:31:19.546Z"",
              ""timestamp"": ""2019-09-15T20:31:19.546Z""
            }
            ]";

        private const string s_set_leverage =
            @"{
              ""account"": 123456,
              ""symbol"": ""XBTUSD"",
              ""currency"": ""XBt"",
              ""underlying"": ""XBT"",
              ""quoteCurrency"": ""USD"",
              ""commission"": 0.00075,
              ""initMarginReq"": 0.1,
              ""maintMarginReq"": 0.005,
              ""riskLimit"": 20000000000,
              ""leverage"": 10,
              ""crossMargin"": false,
              ""deleveragePercentile"": null,
              ""rebalancedPnl"": 3009,
              ""prevRealisedPnl"": -3009,
              ""prevUnrealisedPnl"": 0,
              ""prevClosePrice"": 10314.43,
              ""openingTimestamp"": ""2019-09-15T21:00:00.000Z"",
              ""openingQty"": 0,
              ""openingCost"": 100,
              ""openingComm"": 2909,
              ""openOrderBuyQty"": 0,
              ""openOrderBuyCost"": 0,
              ""openOrderBuyPremium"": 0,
              ""openOrderSellQty"": 0,
              ""openOrderSellCost"": 0,
              ""openOrderSellPremium"": 0,
              ""execBuyQty"": 0,
              ""execBuyCost"": 0,
              ""execSellQty"": 0,
              ""execSellCost"": 0,
              ""execQty"": 0,
              ""execCost"": 0,
              ""execComm"": 0,
              ""currentTimestamp"": ""2019-09-15T21:07:44.078Z"",
              ""currentQty"": 0,
              ""currentCost"": 100,
              ""currentComm"": 2909,
              ""realisedCost"": 100,
              ""unrealisedCost"": 0,
              ""grossOpenCost"": 0,
              ""grossOpenPremium"": 0,
              ""grossExecCost"": 0,
              ""isOpen"": false,
              ""markPrice"": null,
              ""markValue"": 0,
              ""riskValue"": 0,
              ""homeNotional"": 0,
              ""foreignNotional"": 0,
              ""posState"": """",
              ""posCost"": 0,
              ""posCost2"": 0,
              ""posCross"": 0,
              ""posInit"": 0,
              ""posComm"": 0,
              ""posLoss"": 0,
              ""posMargin"": 0,
              ""posMaint"": 0,
              ""posAllowance"": 0,
              ""taxableMargin"": 0,
              ""initMargin"": 0,
              ""maintMargin"": 0,
              ""sessionMargin"": 0,
              ""targetExcessMargin"": 0,
              ""varMargin"": 0,
              ""realisedGrossPnl"": -100,
              ""realisedTax"": 0,
              ""realisedPnl"": -3009,
              ""unrealisedGrossPnl"": 0,
              ""longBankrupt"": 0,
              ""shortBankrupt"": 0,
              ""taxBase"": 0,
              ""indicativeTaxRate"": 0,
              ""indicativeTax"": 0,
              ""unrealisedTax"": 0,
              ""unrealisedPnl"": 0,
              ""unrealisedPnlPcnt"": 0,
              ""unrealisedRoePcnt"": 0,
              ""simpleQty"": null,
              ""simpleCost"": null,
              ""simpleValue"": null,
              ""simplePnl"": null,
              ""simplePnlPcnt"": null,
              ""avgCostPrice"": null,
              ""avgEntryPrice"": null,
              ""breakEvenPrice"": null,
              ""marginCallPrice"": null,
              ""liquidationPrice"": null,
              ""bankruptPrice"": null,
              ""timestamp"": ""2019-09-15T21:07:44.078Z"",
              ""lastPrice"": null,
              ""lastValue"": 0
            }";

        private const string s_bad_currency =
            @"{
              ""error"": {
                ""message"": ""Invalid currency"",
                ""name"": ""HTTPError""
              }
            }";

        private const string s_bad_symbol =
            @"{
              ""error"": {
                ""message"": ""Invalid symbol"",
                ""name"": ""HTTPError""
              }
            }";

        private const string s_bad_orderid =
            @"{
              ""error"": {
                ""message"": ""Invalid orderID"",
                ""name"": ""HTTPError""
              }
            }";

        private const string s_bad_price =
            @"{
              ""error"": {
                ""message"": ""Invalid price"",
                ""name"": ""HTTPError""
              }
            }";

        private const string s_bad_stoppx =
            @"{
              ""error"": {
                ""message"": ""Invalid stopPx for ordType"",
                ""name"": ""HTTPError""
              }
            }";

        private const string s_not_enough_balance =
            @"{
              ""error"": {
                ""message"": ""Account has insufficient Available Balance, 999999 XBt required"",
                ""name"": ""ValidationError""
              }
            }";

        private const string s_bad_leverage =
            @"{
              ""error"": {
                ""message"": ""'leverage' error."",
                ""name"": ""HTTPError""
              }
            }";

        private const string s_not_found =
            @"{
                ""error"": {
                    ""message"":""Not Found"",
                    ""name"":""HTTPError""
                }
            }";

        private const string s_auth_required =
            @"{
                ""error"":{
                    ""message"":""Authorization Required"",
                    ""name"":""HTTPError""
                }
            }";

        private const string s_some_error =
            @"{
                ""error"":{
                    ""message"":""Some error occured"",
                    ""name"":""HTTPError""
                }
            }";

        #endregion

        private bool SetKeys_Method_Called = false;

        public override void SetKeys(string key, string secret)
        {
            SetKeys_Method_Called = true;
        }

        public async override Task<string> Query(string method, string url, Dictionary<string, object> param = null, bool auth = false)
        {
            string response = "";

            if (method == "GET" && url == "/user")
            {
                response = GetUser(param, auth);
            }
            else if (method == "GET" && url == "/user/wallet")
            {
                response = GetWallet(param, auth);
            }
            else if (method == "GET" && url == "/user/margin")
            {
                response = GetMargin(param, auth);
            }
            else if (method == "GET" && url == "/position")
            {
                response = GetPosition(param, auth);
            }
            else if (method == "GET" && url == "/instrument")
            {
                response = GetTicker(param, auth);
            }
            else if (method == "GET" && url == "/order")
            {
                response = GetOrders(param, auth);
            }
            else if (method == "GET" && url == "/orderBook/L2")
            {
                response = GetOrderbook(param, auth);
            }
            else if (method == "GET" && url == "/trade/bucketed")
            {
                response = GetCandles(param, auth);
            }
            else if (method == "POST" && url == "/order")
            {
                response = NewOrder(param, auth);
            }
            else if (method == "PUT" && url == "/order")
            {
                response = AmendOrder(param, auth);
            }
            else if (method == "DELETE" && url == "/order")
            {
                response = Stubbing_Delete_Order_Response(param, auth);
            }
            else if (method == "DELETE" && url == "/order/all")
            {
                response = Stubbing_Delete_Order_All_Response(param, auth);
            }
            else if (method == "POST" && url == "/position/leverage")
            {
                response = Stubbing_Post_Position_Leverage_Response(param, auth);
            }
            else
            {
                response = s_not_found;
            }

            return await Task.FromResult(response);
        }

        private string GetUser(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            return s_get_user;
        }

        private string GetWallet(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("currency") || (string)param["currency"] != "XBt")
                return s_bad_currency;

            return s_get_wallet;
        }

        private string GetMargin(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("currency") || (string)param["currency"] != "XBt")
                return s_bad_currency;

            return s_get_margin;
        }

        private string GetPosition(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("filter") || !((string)param["filter"]).Contains("XBTUSD"))
                return s_bad_symbol;

            return s_get_position;
        }

        private string GetTicker(Dictionary<string, object> param, bool auth)
        {
            if (!param.ContainsKey("symbol") || (string)param["symbol"] != "XBTUSD")
                return s_bad_symbol;

            return s_get_ticker;
        }

        private string GetOrders(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("symbol") || (string)param["symbol"] != "XBTUSD")
                return s_bad_symbol;

            if (param.ContainsKey("filter") && ((string)param["filter"]).Contains("\"orderID\" : \"some_bad_order_id\""))
                return s_bad_orderid;

            if (param.ContainsKey("filter") && ((string)param["filter"]).Contains("\"orderID\" : \"some_order_id\""))
                return s_get_order_with_orderid;

            if (param.ContainsKey("filter") && ((string)param["filter"]).Contains("\"open\" : true"))
                return s_get_open_orders;

            throw new Exception("Unexpected exception");
        }

        private string GetOrderbook(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("symbol") || (string)param["symbol"] != "XBTUSD")
                return s_bad_symbol;

            return s_get_orderbook;
        }

        private string GetCandles(Dictionary<string, object> param, bool auth)
        {
            if (!param.ContainsKey("symbol") || (string)param["symbol"] != "XBTUSD")
                return s_bad_symbol;

            return s_get_candles;
        }

        private string NewOrder(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("symbol") || (string)param["symbol"] != "XBTUSD")
                return s_bad_symbol;

            if (int.Parse((string)param["orderQty"]) > 1000)
                return s_not_enough_balance;

            if ((string)param["ordType"] == "Limit")
            {
                if (!param.ContainsKey("price") || string.IsNullOrEmpty((string)param["price"]))
                    return s_bad_price;

                if (param.ContainsKey("execInst") && ((string)param["execInst"]).Contains("Close"))
                    return s_new_order_close_limit;

                if ((string)param["side"] == "Buy")
                    return s_new_order_limit_buy;

                if ((string)param["side"] == "Sell")
                    return s_new_order_limit_sell;
            }
            else if ((string)param["ordType"] == "Market")
            {
                if (param.ContainsKey("execInst") && ((string)param["execInst"]).Contains("Close"))
                    return s_new_order_close_market;

                if ((string)param["side"] == "Buy")
                    return s_new_order_market_buy;

                if ((string)param["side"] == "Sell")
                    return s_new_order_market_sell;
            }
            else if ((string)param["ordType"] == "StopLimit")
            {
                if (!param.ContainsKey("price") || string.IsNullOrEmpty((string)param["price"]))
                    return s_bad_price;

                if (!param.ContainsKey("stopPx") || string.IsNullOrEmpty((string)param["stopPx"]))
                    return s_bad_stoppx;

                if ((string)param["side"] == "Buy")
                    return s_new_order_stop_limit_buy;

                if ((string)param["side"] == "Sell")
                    return s_new_order_stop_limit_sell;
            }
            else if ((string)param["ordType"] == "Stop")
            {
                if (!param.ContainsKey("stopPx") || string.IsNullOrEmpty((string)param["stopPx"]))
                    return s_bad_stoppx;

                if ((string)param["side"] == "Buy")
                    return s_new_order_stop_market_buy;

                if ((string)param["side"] == "Sell")
                    return s_new_order_stop_market_sell;
            }

            throw new Exception("Unexpected exception");
        }

        private string AmendOrder(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("orderID") || (string)param["orderID"] != "some_order_id")
                return s_bad_orderid;

            return s_amend_order;
        }

        private string Stubbing_Delete_Order_Response(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("orderID") || (string)param["orderID"] != "some_order_id")
                return s_bad_orderid;

            return s_delete_order;
        }

        private string Stubbing_Delete_Order_All_Response(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("symbol") || (string)param["symbol"] != "XBTUSD")
                return s_bad_symbol;

            return s_delete_all_orders;
        }

        private string Stubbing_Post_Position_Leverage_Response(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("symbol") || (string)param["symbol"] != "XBTUSD")
                return s_bad_symbol;

            if (!param.ContainsKey("leverage") || !double.TryParse((string)param["leverage"], out double temp))
                return s_bad_leverage;

            return s_set_leverage;
        }
    }
}

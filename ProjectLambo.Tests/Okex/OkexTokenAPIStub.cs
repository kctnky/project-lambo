using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectLambo.Okex;

namespace ProjectLambo.Tests.Okex
{
    internal class OkexTokenAPIStub : OkexWebRequest
    {
        #region Response Strings

        private const string s_get_accounts =
            @"[
                {
                    ""frozen"":""0"",
                    ""hold"":""0"",
                    ""currency"":""BTC"",
                    ""balance"":""0.0049925"",
                    ""available"":""0.0049925"",
                    ""holds"":""0""
                },
                {
                    ""frozen"":""0"",
                    ""hold"":""0"",
                    ""currency"":""USDT"",
                    ""balance"":""226.74061435"",
                    ""available"":""226.74061435"",
                    ""holds"":""0""
                },
                {
                    ""frozen"":""0"",
                    ""hold"":""0"",
                    ""currency"":""EOS"",
                    ""balance"":""0.4925"",
                    ""available"":""0.4925"",
                    ""holds"":""0""
                }
            ]";

        private const string s_get_ticker =
            @"{
                ""best_ask"":""3858.8"",
                ""best_bid"":""3858.6"",
                ""instrument_id"":""BTC-USDT"",
                ""product_id"":""BTC-USDT"",
                ""last"":""3858.6"",
                ""ask"":""3858.8"",
                ""bid"":""3858.6"",
                ""open_24h"":""3886"",
                ""high_24h"":""3900.8"",
                ""low_24h"":""3839.1"",
                ""base_volume_24h"":""19081.61024493"",
                ""timestamp"":""2019-03-13T11:42:09.849Z"",
                ""quote_volume_24h"":""73978722.5""
            }";

        private const string s_get_order_with_orderid =
            @"{
                ""client_oid"":""oktspot70"",
                ""created_at"":""2019-03-15T02:52:56.000Z"",
                ""filled_notional"":""3.8886"",
                ""filled_size"":""0.001"",
                ""funds"":"""",
                ""instrument_id"":""BTC-USDT"",
                ""notional"":"""",
                ""order_id"":""2482659399697408"",
                ""order_type"":""0"",
                ""price"":""3927.3"",
                ""product_id"":""BTC-USDT"",
                ""side"":""buy"",
                ""size"":""0.001"",
                ""status"":""filled"",
                ""state"": ""2"",     
                ""timestamp"":""2019-03-15T02:52:56.000Z"",
                ""type"":""limit""
            }";

        private const string s_get_pending_orders =
            @"[
                [
                    {
                        ""client_oid"":""oktspot86"",
                        ""created_at"":""2019-03-20T03:28:14.000Z"",
                        ""filled_notional"":""0"",
                        ""filled_size"":""0"",
                        ""funds"":"""",
                        ""instrument_id"":""BTC-USDT"",
                        ""notional"":"""",
                        ""order_id"":""2511109744100352"",
                        ""order_type"":""0"",
                        ""price"":""3594.7"",
                        ""product_id"":""BTC-USDT"",
                        ""side"":""buy"",
                        ""size"":""0.001"",
                        ""status"":""open"",
                        ""state"": ""0"",     
                        ""timestamp"":""2019-03-20T03:28:14.000Z"",
                        ""type"":""limit""
                    },
                    {
                        ""client_oid"":""oktspot85"",
                        ""created_at"":""2019-03-20T03:28:10.000Z"",
                        ""filled_notional"":""0"",
                        ""filled_size"":""0"",
                        ""funds"":"""",
                        ""instrument_id"":""BTC-USDT"",
                        ""notional"":"""",
                        ""order_id"":""2511109459543040"",
                        ""order_type"":""0"",
                        ""price"":""3594.9"",
                        ""product_id"":""BTC-USDT"",
                        ""side"":""buy"",
                        ""size"":""0.001"",
                        ""status"":""open"",
                        ""state"": ""0"",     
                        ""timestamp"":""2019-03-20T03:28:10.000Z"",
                        ""type"":""limit""
                    }
                ],
                {
                    ""before"":""2511109744100352"",
                    ""after"":""2511109459543040""
                }
            ]";

        private const string s_get_orderbook =
            @"{
                ""asks"":[
                    [
                        ""3993.2"",
                        ""0.41600068"",
                        ""1""
                    ],
                    [
                        ""3993.4"",
                        ""1.24807818"",
                        ""3""
                    ],
                    [
                        ""3993.6"",
                        ""0.03"",
                        ""1""
                    ],
                    [
                        ""3993.8"",
                        ""0.03"",
                        ""1""
                    ]
                ],
                ""bids"":[
                    [
                        ""3993"",
                        ""0.15149658"",
                        ""2""
                    ],
                    [
                        ""3992.8"",
                        ""1.19046818"",
                        ""1""
                    ],
                    [
                        ""3992.6"",
                        ""0.20831389"",
                        ""1""
                    ],
                    [
                        ""3992.4"",
                        ""0.01669446"",
                        ""2""
                    ]
                ],
                ""timestamp"":""2019-03-20T03:55:37.888Z""
            }";

        private const string s_get_candles =
            @"[
                [
                    ""2019-03-19T16:00:00.000Z"",
                    ""3997.3"",
                    ""4031.9"",
                    ""3982.5"",
                    ""3998.7"",
                    ""26175.21141385""
                ],
                [
                    ""2019-03-18T16:00:00.000Z"",
                    ""3980.6"",
                    ""4014.6"",
                    ""3968.9"",
                    ""3997.3"",
                    ""33053.48725643""
                ]
            ]";

        private const string s_new_order_limit_buy =
            @"{
                ""client_oid"":""oktspot79"",
                ""error_code"":"""",
                ""error_message"":"""",
                ""order_id"":""2510789768709120"",
                ""result"":true
            }";

        private const string s_new_order_limit_sell =
            @"{
                ""client_oid"":""oktspot79"",
                ""error_code"":"""",
                ""error_message"":"""",
                ""order_id"":""2510789768709120"",
                ""result"":true
            }";

        private const string s_new_order_market_buy =
            @"{
                ""client_oid"":""oktspot79"",
                ""error_code"":"""",
                ""error_message"":"""",
                ""order_id"":""2510789768709120"",
                ""result"":true
            }";

        private const string s_new_order_market_sell =
            @"{
                ""client_oid"":""oktspot79"",
                ""error_code"":"""",
                ""error_message"":"""",
                ""order_id"":""2510789768709120"",
                ""result"":true
            }";

        private const string s_cancel_order_with_orderid =
            @"{
                ""btc-usdt"":[
                    {
                        ""result"":true,
                        ""client_oid"":""a123"",
                        ""order_id"": ""2510832677225473""
                    }
                ]
            }";

        private const string s_some_error_message =
            @"{
                ""error_code"":""999"",
                ""message"":""Some error message will replace this""
            }";

        private const string s_auth_required =
            @"{
                ""code"":30001,
                ""message"":""OK-ACCESS-KEY header is required""
            }";

        private const string s_not_found =
            @"{
                ""code"":404,
                ""data"":{},
                ""detailMsg"":""No message available"",
                ""msg"":""Not Found""
            }";

        #endregion

        public OkexTokenAPIStub() 
            : base("/api/spot/v3") 
        { }

        private bool SetKeys_Method_Called = false;
        public override void SetKeys(string key, string secret, string passphrase)
        {
            SetKeys_Method_Called = true;
        }

        public async override Task<string> Query(string method, string url, Dictionary<string, object> param = null, bool auth = false)
        {
            string response = "";

            var urlparts = url.Split('/');

            if (method == "GET" && urlparts.Length == 2 && urlparts[1] == "accounts")
            {
                response = GetAccounts(param, auth);
            }
            else if (method == "GET" && urlparts.Length == 4 && urlparts[1] == "instruments" && urlparts[3] == "ticker")
            {
                var symbol = urlparts[2];
                response = GetTicker(param, auth, symbol);
            }
            else if (method == "GET" && urlparts.Length == 3 && urlparts[1] == "orders")
            {
                var orderId = urlparts[2];
                response = GetOrderWithOrderID(param, auth, orderId);
            }
            else if (method == "GET" && urlparts.Length == 2 && urlparts[1] == "orders_pending")
            {
                response = GetPendingOrders(param, auth);
            }
            else if (method == "GET" && urlparts.Length == 4 && urlparts[1] == "instruments" && urlparts[3] == "book")
            {
                var symbol = urlparts[2];
                response = GetOrderbook(param, auth, symbol);
            }
            else if (method == "GET" && urlparts.Length == 4 && urlparts[1] == "instruments" && urlparts[3] == "candles")
            {
                var symbol = urlparts[2];
                response = GetCandles(param, auth, symbol);
            }
            else if (method == "POST" && urlparts.Length == 2 && urlparts[1] == "orders")
            {
                response = NewOrder(param, auth);
            }
            else if (method == "POST" && urlparts.Length == 3 && urlparts[1] == "cancel_orders")
            {
                var orderId = urlparts[2];
                response = CancelOrderWithOrderID(param, auth, orderId);
            }
            else
            {
                response = s_not_found;
            }

            return await Task.FromResult(response);
        }

        private string GetAccounts(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            return s_get_accounts;
        }

        private string GetTicker(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (symbol != "BTC-USDT")
                return s_some_error_message;

            return s_get_ticker;
        }

        private string GetOrderWithOrderID(Dictionary<string, object> param, bool auth, string orderId)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("instrument_id") || (string)param["instrument_id"] != "BTC-USDT")
                return s_some_error_message;

            if (orderId != "some_order_id")
                return s_some_error_message;

            return s_get_order_with_orderid;
        }

        private string GetPendingOrders(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("instrument_id") || (string)param["instrument_id"] != "BTC-USDT")
                return s_some_error_message;

            return s_get_pending_orders;
        }

        private string GetOrderbook(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (symbol != "BTC-USDT")
                return s_some_error_message;

            if (param.ContainsKey("size") && !int.TryParse((string)param["size"], out int temp))
                return s_some_error_message;

            return s_get_orderbook;
        }

        private string GetCandles(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (symbol != "BTC-USDT")
                return s_some_error_message;

            return s_get_candles;
        }

        private string NewOrder(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("instrument_id") || (string)param["instrument_id"] != "BTC-USDT")
                return s_some_error_message;

            if (param.ContainsKey("size") && int.Parse((string)param["size"]) > 1000)
                return s_some_error_message;

            if (param.ContainsKey("notional") && int.Parse((string)param["notional"]) > 1000)
                return s_some_error_message;

            if ((string)param["type"] == "limit")
            {
                if (!param.ContainsKey("price") || string.IsNullOrEmpty((string)param["price"]))
                    return s_some_error_message;

                if ((string)param["side"] == "buy")
                    return s_new_order_limit_buy;

                if ((string)param["side"] == "sell")
                    return s_new_order_limit_sell;
            }
            else if ((string)param["type"] == "market")
            {
                if ((string)param["side"] == "buy")
                    return s_new_order_market_buy;

                if ((string)param["side"] == "sell")
                    return s_new_order_market_sell;
            }

            throw new Exception("Unexpected exception");
        }

        private string CancelOrderWithOrderID(Dictionary<string, object> param, bool auth, string orderId)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (orderId != "some_order_id")
                return s_some_error_message;

            if (!param.ContainsKey("instrument_id") || (string)param["instrument_id"] != "BTC-USDT")
                return s_some_error_message;

            return s_cancel_order_with_orderid;
        }
    }
}

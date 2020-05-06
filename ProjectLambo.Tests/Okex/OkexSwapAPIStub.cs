using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectLambo.Okex;

namespace ProjectLambo.Tests.Okex
{
    internal class OkexSwapAPIStub : OkexWebRequest
    {
        #region Response Strings

        private const string s_get_accounts =
            @"{
                ""info"":{
                    ""equity"":""3.0117"",
                    ""fixed_balance"":""0.0000"",
                    ""instrument_id"":""BTC-USD-SWAP"",
                    ""margin"":""0.5519"",
                    ""margin_frozen"":""0.0000"",
                    ""margin_mode"":""crossed"",
                    ""margin_ratio"":""1.0913"",
                    ""realized_pnl"":""-0.0006"",
                    ""timestamp"":""2019-03-25T03:46:10.336Z"",
                    ""total_avail_balance"":""3.0000"",
                    ""unrealized_pnl"":""0.0123""
                }
            }";

        private const string s_get_position =
            @"{
              ""margin_mode"": ""crossed"",
              ""timestamp"": null,
              ""holding"": [
                {
                  ""avail_position"": ""0"",
                  ""avg_cost"": ""0"",
                  ""instrument_id"": ""BTC-USD-SWAP"",
                  ""last"": """",
                  ""leverage"": ""0"",
                  ""liquidation_price"": ""0"",
                  ""maint_margin_ratio"": ""0"",
                  ""margin"": ""0"",
                  ""position"": ""0"",
                  ""realized_pnl"": ""0"",
                  ""settled_pnl"": ""0"",
                  ""settlement_price"": ""0"",
                  ""side"": ""long"",
                  ""timestamp"": ""1970-01-01T00:00:00Z""
                },
                {
                  ""avail_position"": ""1"",
                  ""avg_cost"": ""10000"",
                  ""instrument_id"": ""BTC-USD-SWAP"",
                  ""last"": """",
                  ""leverage"": ""5"",
                  ""liquidation_price"": ""0"",
                  ""maint_margin_ratio"": ""0"",
                  ""margin"": ""0"",
                  ""position"": ""0"",
                  ""realized_pnl"": ""0"",
                  ""settled_pnl"": ""0"",
                  ""settlement_price"": ""0"",
                  ""side"": ""short"",
                  ""timestamp"": ""1970-01-01T00:00:00Z""
                }
              ]
            }";

        private const string s_get_ticker =
            @"{
                ""instrument_id"":""BTC-USD-SWAP"",
                ""last"":""3.611"",
                ""high_24h"":""3.665"",
                ""low_24h"":""3.57"",
                ""volume_24h"":""1733788"",
                ""best_ask"":""3.611"",
                ""best_bid"":""3.61"",
                ""timestamp"":""2019-03-25T09:16:07.501Z""
            }";

        private const string s_get_order_with_orderid =
            @"{
                ""filled_qty"":""1"",
                ""fee"":""-0.000550"",
                ""client_oid"":"""",
                ""price_avg"":""3.640"",
                ""type"":""2"",
                ""instrument_id"":""BTC-USD-SWAP"",
                ""size"":""1"",
                ""price"":""3.640"",
                ""contract_val"":""10"",
                ""order_id"":""6a-8-54cee3a3f-0"",
                ""order_type"":""0"",
                ""state"":""2"",
                ""state"": ""2"",    
                ""timestamp"":""2019-03-25T03:45:17.376Z""
            }";

        private const string s_get_openorders =
            @"{
                ""order_info"":[
                    {
                        ""client_oid"":"""",
                        ""contract_val"":""10"",
                        ""fee"":""-0.000551"",
                        ""filled_qty"":""1"",
                        ""instrument_id"":""BTC-USD-SWAP"",
                        ""order_id"":""6a-7-54d663a28-0"",
                        ""order_type"":""0"",
                        ""price"":""3.633"",
                        ""price_avg"":""3.633"",
                        ""size"":""1"",
                        ""status"":""2"",
                        ""state"": ""2"",    
                        ""timestamp"":""2019-03-25T05:56:21.674Z"",
                        ""type"":""4""
                    },
                    {
                        ""client_oid"":"""",
                        ""contract_val"":""10"",
                        ""fee"":""-0.000550"",
                        ""filled_qty"":""1"",
                        ""instrument_id"":""BTC-USD-SWAP"",
                        ""order_id"":""6a-8-54cee3a3f-0"",
                        ""order_type"":""0"",
                        ""price"":""3.640"",
                        ""price_avg"":""3.640"",
                        ""size"":""1"",
                        ""status"":""2"",
                        ""state"": ""2"",    
                        ""timestamp"":""2019-03-25T03:45:17.376Z"",
                        ""type"":""2""
                    }
                ]
            }";

        private const string s_get_orderbook =
            @"{
                ""asks"":[
                    [
                        ""3968.5"",
                        ""121"",
                        0,
                        2
                    ],
                    [
                        ""3968.6"",
                        ""160"",
                        0,
                        4
                    ]
                ],
                ""bids"":[
                    [
                        ""3968.4"",
                        ""179"",
                        0,
                        4
                    ],
                    [
                        ""3968"",
                        ""914"",
                        0,
                        3
                    ]
                ],
                ""time"":""2019-03-25T11:12:10.601Z""
            }";

        private const string s_get_candles =
            @"[
                [
                    ""2019-03-25T16:00:00.000Z"",
                    ""3946.6"",
                    ""3960"",
                    ""3860"",
                    ""3917.7"",
                    ""329627"",
                    ""8434.421""
                ],
                [
                    ""2019-03-24T16:00:00.000Z"",
                    ""3971.4"",
                    ""3977.7"",
                    ""3942.6"",
                    ""3946.4"",
                    ""333780"",
                    ""8416.0873""
                ]
            ]";

        private const string s_new_order_open_long =
            @"{
                ""error_message"":"""",
                ""result"":""true"",
                ""error_code"":""0"",
                ""client_oid"":""oktswap6"",
                ""order_id"":""6a-d-54dcc6543-0""
            }";

        private const string s_new_order_open_short =
            @"{
                ""error_message"":"""",
                ""result"":""true"",
                ""error_code"":""0"",
                ""client_oid"":""oktswap6"",
                ""order_id"":""6a-d-54dcc6543-0""
            }";

        private const string s_new_order_close_long =
            @"{
                ""error_message"":"""",
                ""result"":""true"",
                ""error_code"":""0"",
                ""client_oid"":""oktswap6"",
                ""order_id"":""6a-d-54dcc6543-0""
            }";

        private const string s_new_order_close_short =
            @"{
                ""error_message"":"""",
                ""result"":""true"",
                ""error_code"":""0"",
                ""client_oid"":""oktswap6"",
                ""order_id"":""6a-d-54dcc6543-0""
            }";

        private const string s_cancel_order_with_orderid =
            @"{
                ""result"":""true"",
                ""client_oid"":""oktswap6"",
                ""order_id"":""6a-d-54dcc6543-0""
            }";

        private const string s_set_leverage =
            @"{
                ""long_leverage"":""10.0000"",
                ""short_leverage"":""10.0000"",
                ""margin_mode"":""crossed"",
                ""instrument_id"":""BTC-USD-SWAP""
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

        public OkexSwapAPIStub()
            : base("/api/swap/v3")
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

            if (method == "GET" && urlparts.Length == 3 && urlparts[2] == "accounts")
            {
                var symbol = urlparts[1];
                response = GetAccounts(param, auth, symbol);
            }
            else if (method == "GET" && urlparts.Length == 3 && urlparts[2] == "position")
            {
                var symbol = urlparts[1];
                response = GetPosition(param, auth, symbol);
            }
            else if (method == "GET" && urlparts.Length == 4 && urlparts[1] == "instruments" && urlparts[3] == "ticker")
            {
                var symbol = urlparts[2];
                response = GetTicker(param, auth, symbol);
            }
            else if (method == "GET" && urlparts.Length == 4 && urlparts[1] == "orders")
            {
                var symbol = urlparts[2];
                var orderId = urlparts[3];
                response = GetOrderWithOrderID(param, auth, symbol, orderId);
            }
            else if (method == "GET" && urlparts.Length == 3 && urlparts[1] == "orders")
            {
                var symbol = urlparts[2];
                response = GetOpenOrders(param, auth, symbol);
            }
            else if (method == "GET" && urlparts.Length == 4 && urlparts[1] == "instruments" && urlparts[3] == "depth")
            {
                var symbol = urlparts[2];
                response = GetOrderbook(param, auth, symbol);
            }
            else if (method == "GET" && urlparts.Length == 4 && urlparts[1] == "instruments" && urlparts[3] == "candles")
            {
                var symbol = urlparts[2];
                response = GetCandles(param, auth, symbol);
            }
            else if (method == "POST" && urlparts.Length == 2 && urlparts[1] == "order")
            {
                response = NewOrder(param, auth);
            }
            else if (method == "POST" && urlparts.Length == 4 && urlparts[1] == "cancel_order")
            {
                var symbol = urlparts[2];
                var orderId = urlparts[3];
                response = CancelOrderWithOrderID(param, auth, symbol, orderId);
            }
            else if (method == "POST" && urlparts.Length == 4 && urlparts[1] == "accounts" && urlparts[3] == "leverage")
            {
                var symbol = urlparts[2];
                response = SetLeverage(param, auth, symbol);
            }
            else
            {
                response = s_not_found;
            }

            return await Task.FromResult(response);
        }

        private string GetAccounts(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (symbol != "BTC-USD-SWAP")
                return s_some_error_message;

            return s_get_accounts;
        }

        private string GetPosition(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (symbol != "BTC-USD-SWAP")
                return s_some_error_message;

            return s_get_position;
        }

        private string GetTicker(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (symbol != "BTC-USD-SWAP")
                return s_some_error_message;

            return s_get_ticker;
        }

        private string GetOrderWithOrderID(Dictionary<string, object> param, bool auth, string symbol, string orderId)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (symbol != "BTC-USD-SWAP")
                return s_some_error_message;

            if (orderId != "some_order_id")
                return s_some_error_message;

            return s_get_order_with_orderid;
        }

        private string GetOpenOrders(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (symbol != "BTC-USD-SWAP")
                return s_some_error_message;

            return s_get_openorders;
        }

        private string GetOrderbook(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (symbol != "BTC-USD-SWAP")
                return s_some_error_message;

            if (param.ContainsKey("size") && !int.TryParse((string)param["size"], out int temp))
                return s_some_error_message;

            return s_get_orderbook;
        }

        private string GetCandles(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (symbol != "BTC-USD-SWAP")
                return s_some_error_message;

            return s_get_candles;
        }

        private string NewOrder(Dictionary<string, object> param, bool auth)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (!param.ContainsKey("instrument_id") || (string)param["instrument_id"] != "BTC-USD-SWAP")
                return s_some_error_message;

            if (param.ContainsKey("size") && int.Parse((string)param["size"]) > 1000)
                return s_some_error_message;

            if (param.ContainsKey("notional") && int.Parse((string)param["notional"]) > 1000)
                return s_some_error_message;

            if (!param.ContainsKey("price") || string.IsNullOrEmpty((string)param["price"]))
                return s_some_error_message;

            if ((string)param["type"] == "1")
            {
                return s_new_order_open_long;
            }
            else if ((string)param["type"] == "2")
            {
                return s_new_order_open_short;
            }
            else if ((string)param["type"] == "3")
            {
                return s_new_order_close_long;
            }
            else if ((string)param["type"] == "4")
            {
                return s_new_order_close_short;
            }

            throw new Exception("Unexpected exception");
        }

        private string CancelOrderWithOrderID(Dictionary<string, object> param, bool auth, string symbol, string orderId)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (symbol != "BTC-USD-SWAP")
                return s_some_error_message;

            if (orderId != "some_order_id")
                return s_some_error_message;

            return s_cancel_order_with_orderid;
        }

        private string SetLeverage(Dictionary<string, object> param, bool auth, string symbol)
        {
            if (!auth)
                return s_auth_required;

            if (!SetKeys_Method_Called)
                return s_auth_required;

            if (symbol != "BTC-USD-SWAP")
                return s_some_error_message;

            if (!param.ContainsKey("leverage") || !double.TryParse((string)param["leverage"], out double leverage))
                return s_some_error_message;

            if (!param.ContainsKey("side") || !double.TryParse((string)param["side"], out double side))
                return s_some_error_message;

            return s_set_leverage;
        }
    }
}

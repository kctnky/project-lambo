using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectLambo.Okex.Models;

namespace ProjectLambo.Okex
{
    /// <summary>
    /// This class implements Okex API endpoints for token trading v3 API.
    /// </summary>
    public class OkexTokenAPI
    {
        #region Fields

        private readonly OkexWebRequest request;

        #endregion

        #region Constructers

        /// <summary>
        /// Default constructer.
        /// </summary>
        public OkexTokenAPI()
        {
            request = new OkexWebRequest("/api/spot/v3");
        }

        /// <summary>
        /// Constructer.
        /// </summary>
        /// <param name="apiKey">key</param>
        /// <param name="apiSecret">secret</param>
        /// <param name="apiPassphrase">passphrase</param>
        public OkexTokenAPI(string apiKey, string apiSecret, string apiPassphrase)
        {
            request = new OkexWebRequest("/api/spot/v3", apiKey, apiSecret, apiPassphrase);
        }

        /// <summary>
        /// Constructer with dependency injection for unit test project.
        /// </summary>
        /// <param name="request"></param>
        internal OkexTokenAPI(OkexWebRequest request)
        {
            this.request = request;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set API access keys for exchange.
        /// </summary>
        /// <param name="apiKey">key</param>
        /// <param name="apiSecret">secret</param>
        /// <param name="apiPassphrase">passphrase</param>
        public void SetKeys(string apiKey, string apiSecret, string apiPassphrase)
        {
            request.SetKeys(apiKey, apiSecret, apiPassphrase);
        }

        /// <summary>
        /// Request wallet information of authenticated account.
        /// </summary>
        /// <returns>Wallet</returns>
        public Wallet GetWallet()
            => GetWalletAsync().Result;

        /// <summary>
        /// <see cref="GetWallet"/>
        /// </summary>
        public async Task<Wallet> GetWalletAsync()
        {
            string apiUrl = "/accounts";
            string response = await request.Query("GET", apiUrl, null, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var wallet = new Wallet();

            JArray jArray = jsonResponse as JArray;
            if (jArray.Count == 0)
                return wallet; // user has not any token in wallet 

            try
            {
                foreach (var jToken in jArray)
                {
                    wallet.Currencies.Add(new Holding()
                    {
                        Currency = jToken["currency"].Value<string>(),
                        Balance = jToken["balance"].Value<double>(),
                        Available = jToken["available"].Value<double>()
                    });
                }
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            //TODO: Calculate USD and BTC equivalents of wallet by also requesting ticker information.
            //wallet.BtcEquivalent = null;
            //wallet.UsdEquivalent = null;

            return wallet;
        }

        /// <summary>
        /// Request information for the given symbol of trading.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <returns>Ticker</returns>
        public Ticker GetTicker(string symbol)
            => GetTickerAsync(symbol).Result;

        /// <summary>
        /// <see cref="GetTicker(string)"/>
        /// </summary>
        public async Task<Ticker> GetTickerAsync(string symbol)
        {
            string apiUrl = "/instruments/" + symbol + "/ticker";
            string response = await request.Query("GET", apiUrl, null, false).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var ticker = new Ticker();

            try
            {
                ticker.Symbol = jsonResponse["instrument_id"].Value<string>();
                ticker.LastPrice = jsonResponse["last"].Value<double>();
                ticker.Timestamp = DateTime.SpecifyKind(jsonResponse["timestamp"].Value<DateTime>(), DateTimeKind.Utc);
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            // Okex symbols are defined in the format of "<BaseCurrency>-<QuoteCurrency>"
            // So, following code sets these values from the symbol string given as input argument.
            // However, you can check true values of these fields by requesting from /api/spot/v3/instruments api endpoint

            var parts = symbol.Split('-');
            if (parts.Length >= 2)
            {
                ticker.BaseCurrency = parts[0];
                ticker.QuoteCurrency = parts[1];
            }

            return ticker;
        }

        /// <summary>
        /// Request details for the order with given orderID.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="orderID">orderID</param>
        /// <returns>Order</returns>
        public Order GetOrder(string symbol, string orderID)
            => GetOrderAsync(symbol, orderID).Result;

        /// <summary>
        /// <see cref="GetOrder(string, string)"/>
        /// </summary>
        public async Task<Order> GetOrderAsync(string symbol, string orderID)
        {
            var param = new Dictionary<string, object>();
            param["instrument_id"] = symbol;

            string apiUrl = "/orders/" + orderID;
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var order = new Order();

            try
            {
                string ordStatus = jsonResponse["state"].Value<string>();
                string ordType = jsonResponse["type"].Value<string>();
                string side = jsonResponse["side"].Value<string>();

                order.Symbol = jsonResponse["instrument_id"].Value<string>();
                order.OrderID = jsonResponse["order_id"].Value<string>();
                order.Size = jsonResponse["size"].Value<double>();
                order.FilledQuantity = jsonResponse["filled_size"].Value<double>();
                order.RemainingQuantity = order.Size - order.FilledQuantity;

                order.Price = jsonResponse["price"] == null ? null
                    : jsonResponse["price"].Type == JTokenType.Null ? null
                    : (double?)jsonResponse["price"].Value<double>();

                order.Side = side.Equals("buy") ? OrderSide.Buy
                    : side.Equals("sell") ? OrderSide.Sell
                    : throw new Exception();

                order.Type = ordType.Equals("limit") ? OrderType.Limit
                    : ordType.Equals("market") ? OrderType.Market
                    : throw new Exception();

                // For simplicity, order status is assumed to be in one of three states: Open, Cancelled, Filled
                order.Status = ordStatus.Equals("-2") ? OrderStatus.Cancelled
                    : ordStatus.Equals("-1") ? OrderStatus.Cancelled
                    : ordStatus.Equals("0") ? OrderStatus.Open
                    : ordStatus.Equals("1") ? OrderStatus.Open
                    : ordStatus.Equals("2") ? OrderStatus.Filled
                    : ordStatus.Equals("3") ? OrderStatus.Open
                    : ordStatus.Equals("4") ? OrderStatus.Cancelled
                    : throw new Exception();
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            return order;
        }

        /// <summary>
        /// Request all open orders for the given symbol of trading.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="max">maximum number of orders to be returned</param>
        /// <returns>a list of Order type</returns>
        public IList<Order> GetOpenOrders(string symbol, int max = 50)
            => GetOpenOrdersAsync(symbol, max).Result;

        /// <summary>
        /// <see cref="GetOpenOrders(string, int)"/>
        /// </summary>
        public async Task<IList<Order>> GetOpenOrdersAsync(string symbol, int max = 50)
        {
            var param = new Dictionary<string, object>();
            param["instrument_id"] = symbol;
            param["limit"] = max.ToString();

            string apiUrl = "/orders_pending";
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            IList<Order> orderList = new List<Order>();

            JArray jArray = jsonResponse as JArray;
            if (jArray.Count == 0)
                return orderList; // user has not any open order 

            try
            {
                foreach (var jToken in jArray[0])
                {
                    var order = new Order();

                    string ordStatus = jToken["state"].Value<string>();
                    string ordType = jToken["type"].Value<string>();
                    string side = jToken["side"].Value<string>();

                    order.Symbol = jToken["instrument_id"].Value<string>();
                    order.OrderID = jToken["order_id"].Value<string>();
                    order.Size = jToken["size"].Value<double>();
                    order.FilledQuantity = jToken["filled_size"].Value<double>();
                    order.RemainingQuantity = order.Size - order.FilledQuantity;

                    order.Price = jToken["price"] == null ? null
                        : jToken["price"].Type == JTokenType.Null ? null
                        : (double?)jToken["price"].Value<double>();

                    order.Side = side.Equals("buy") ? OrderSide.Buy
                        : side.Equals("sell") ? OrderSide.Sell
                        : throw new Exception();

                    order.Type = ordType.Equals("limit") ? OrderType.Limit
                        : ordType.Equals("market") ? OrderType.Market
                        : throw new Exception();

                    // For simplicity, order status is assumed to be in one of three states: Open, Cancelled, Filled
                    order.Status = ordStatus.Equals("-2") ? OrderStatus.Cancelled
                        : ordStatus.Equals("-1") ? OrderStatus.Cancelled
                        : ordStatus.Equals("0") ? OrderStatus.Open
                        : ordStatus.Equals("1") ? OrderStatus.Open
                        : ordStatus.Equals("2") ? OrderStatus.Filled
                        : ordStatus.Equals("3") ? OrderStatus.Open
                        : ordStatus.Equals("4") ? OrderStatus.Cancelled
                        : throw new Exception();

                    orderList.Add(order);
                }
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            return orderList;
        }

        /// <summary>
        /// Request orderbook for the given symbol of trading.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="depth">depth of the orderbook for each side</param>
        /// <returns>a list of RawOrder type</returns>
        public IList<RawOrder> GetOrderBook(string symbol, int depth = 20)
            => GetOrderBookAsync(symbol, depth).Result;

        /// <summary>
        /// <see cref="GetOrderBook(string, int)"/>
        /// </summary>
        public async Task<IList<RawOrder>> GetOrderBookAsync(string symbol, int depth = 20)
        {
            var param = new Dictionary<string, object>();
            param["size"] = depth.ToString();

            string apiUrl = "/instruments/" + symbol + "/book";
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            IList<RawOrder> orderBook = new List<RawOrder>();

            JArray asks = jsonResponse["asks"] as JArray;
            JArray bids = jsonResponse["bids"] as JArray;

            try
            {
                for (int i = asks.Count - 1; i >= 0; i--)
                {
                    var rawOrder = new RawOrder();

                    JArray raw = asks[i] as JArray;
                    rawOrder.Price = raw[0].Value<double>();
                    rawOrder.Size = raw[1].Value<double>();
                    rawOrder.Side = OrderSide.Sell;

                    orderBook.Add(rawOrder);
                }

                for (int i = 0; i <= bids.Count - 1; i++)
                {
                    var rawOrder = new RawOrder();

                    JArray raw = bids[i] as JArray;
                    rawOrder.Price = raw[0].Value<double>();
                    rawOrder.Size = raw[1].Value<double>();
                    rawOrder.Side = OrderSide.Buy;

                    orderBook.Add(rawOrder);
                }
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            return orderBook;
        }

        /// <summary>
        /// Request historical data for the given symbol of trading.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="type">timespan for each candle</param>
        /// <param name="start">starting time of the first candle to be returned (UTC+0)</param>
        /// <param name="end">starting time of the last candle to be returned (UTC+0)</param>
        /// <returns>a list of Candle type</returns>
        public IList<Candle> GetCandlesticks(string symbol, CandleType type, DateTime start, DateTime end)
            => GetCandlesticksAsync(symbol, type, start, end).Result;

        /// <summary>
        /// <see cref="GetCandlesticks(string, CandleType, DateTime, DateTime)"/>
        /// </summary>
        public async Task<IList<Candle>> GetCandlesticksAsync(string symbol, CandleType type, DateTime start, DateTime end)
        {
            var param = new Dictionary<string, object>();
            param["start"] = start.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            param["end"] = end.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            param["granularity"] = type == CandleType.OneMinute ? "60"
                : type == CandleType.FiveMinutes ? "300"
                : type == CandleType.FifteenMinutes ? "900"
                : type == CandleType.ThirtyMinutes ? "1800"
                : type == CandleType.OneHour ? "3600"
                : type == CandleType.TwoHours ? "7200"
                : type == CandleType.FourHours ? "14400"
                : type == CandleType.OneDay ? "86400"
                : throw new OkexUnexpectedArgument();

            string apiUrl = "/instruments/" + symbol + "/candles";
            string response = await request.Query("GET", apiUrl, param, false).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            IList<Candle> candles = new List<Candle>();

            JArray jArray = jsonResponse as JArray;
            if (jArray.Count == 0)
                return candles; // did not return any candle 

            try
            {
                for (int i = jArray.Count - 1; i >= 0; i--)
                {
                    var candle = new Candle();

                    var raw = jArray[i] as JArray;
                    candle.Symbol = symbol;
                    candle.Timestamp = DateTime.SpecifyKind(raw[0].Value<DateTime>(), DateTimeKind.Utc);
                    candle.Open = raw[1].Value<double>();
                    candle.High = raw[2].Value<double>();
                    candle.Low = raw[3].Value<double>();
                    candle.Close = raw[4].Value<double>();
                    candle.Volume = raw[5].Value<double>();
                    candle.Type = type; // set what is passed as input

                    candles.Add(candle);
                }
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            return candles;
        }

        /// <summary>
        /// Put a new order for the given symbol of trading.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="type">order type</param>
        /// <param name="quantity">order quantity</param>
        /// <param name="side">order side</param>
        /// <param name="price">price (only for limit orders)</param>
        /// <returns>orderID</returns>
        public string NewOrder(string symbol, OrderType type, double quantity, OrderSide side, double? price = null)
            => NewOrderAsync(symbol, type, quantity, side, price).Result;

        /// <summary>
        /// <see cref="NewOrder(string, OrderType, double, OrderSide, double?)"/>
        /// </summary>
        public async Task<string> NewOrderAsync(string symbol, OrderType type, double quantity, OrderSide side, double? price = null)
        {
            var param = new Dictionary<string, object>();

            param["instrument_id"] = symbol;

            param["type"] = type == OrderType.Limit ? "limit"
                : type == OrderType.Market ? "market"
                : throw new OkexUnexpectedArgument();

            param["side"] = side == OrderSide.Buy ? "buy"
                : side == OrderSide.Sell ? "sell"
                : throw new OkexUnexpectedArgument();

            if (type == OrderType.Limit || (type == OrderType.Market && side == OrderSide.Sell))
                param["size"] = quantity.ToString();

            if (type == OrderType.Market && side == OrderSide.Buy)
                param["notional"] = quantity.ToString();

            if (type == OrderType.Limit)
                param["price"] = price.ToString();

            string apiUrl = "/orders";
            string response = await request.Query("POST", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            try
            {
                return jsonResponse["order_id"].Value<string>();
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }
        }

        /// <summary>
        /// Cancel an existing order with orderID.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <param name="orderID">orderID</param>
        public void CancelOrder(string symbol, string orderID)
            => CancelOrderAsync(symbol, orderID).Wait();

        /// <summary>
        /// <see cref="CancelOrder(string, string)"/>
        /// </summary>
        public async Task CancelOrderAsync(string symbol, string orderID)
        {
            var param = new Dictionary<string, object>();
            param["instrument_id"] = symbol;

            string apiUrl = "/cancel_orders/" + orderID;
            string response = await request.Query("POST", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);
        }

        #endregion

        #region Private Methods

        /* This method checks if response contains any error such that api returns with error code.
         * If response contains an error then regarding information is encapsulated and ExchangeAPIError is thrown.
         */
        protected void ValidateResponse(string apiurl, JToken token)
        {
            if (!(token is JArray))
            {
                // error message may contain "code" in response to some requests
                if (token["code"] != null && token["code"].Value<string>() != string.Empty)
                {
                    int code = int.Parse(token["code"].Value<string>());
                    string message = token["error_message"] != null ? token["error_message"].Value<string>()
                        : token["message"] != null ? token["message"].Value<string>()
                        : token["msg"] != null ? token["msg"].Value<string>()
                        : "Unknown error";
                    if (code != 0)
                        throw new OkexAPIError(string.Format("Request to {0} returned error", apiurl), code, null, message);
                }

                // error message may contain "error_code" in response to some requests
                if (token["error_code"] != null && token["error_code"].Value<string>() != string.Empty)
                {
                    int code = int.Parse(token["error_code"].Value<string>());
                    string message = token["error_message"] != null ? token["error_message"].Value<string>()
                        : token["message"] != null ? token["message"].Value<string>()
                        : token["msg"] != null ? token["msg"].Value<string>()
                        : "Unknown error";
                    if (code != 0)
                        throw new OkexAPIError(string.Format("Request to {0} returned error", apiurl), code, null, message);
                }
            }
        }

        #endregion
    }
}

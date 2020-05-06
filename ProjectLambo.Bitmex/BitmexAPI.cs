using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectLambo.Bitmex.Models;

namespace ProjectLambo.Bitmex
{
    /// <summary>
    /// This class implements Bitmex API endpoints.
    /// </summary>
    public class BitmexAPI
    {
        #region Fields

        private readonly BitmexWebRequest request;

        #endregion

        #region Constructers

        /// <summary>
        /// Default constructer.
        /// </summary>
        public BitmexAPI()
        {
            request = new BitmexWebRequest();
        }

        /// <summary>
        /// Constructer.
        /// </summary>
        /// <param name="apiKey">key</param>
        /// <param name="apiSecret">secret</param>
        public BitmexAPI(string apiKey, string apiSecret)
        {
            request = new BitmexWebRequest(apiKey, apiSecret);
        }

        /// <summary>
        /// Constructer with dependency injection for unit test project.
        /// </summary>
        /// <param name="request"></param>
        internal BitmexAPI(BitmexWebRequest request)
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
        public void SetKeys(string apiKey, string apiSecret)
        {
            request.SetKeys(apiKey, apiSecret);
        }

        /// <summary>
        /// Request user information of authenticated account.
        /// </summary>
        /// <returns>UserInfo</returns>
        public User GetUser()
            => GetUserAsync().Result;

        /// <summary>
        /// <see cref="GetUser"/>
        /// </summary>
        public async Task<User> GetUserAsync()
        {
            string apiUrl = "/user";
            string response = await request.Query("GET", apiUrl, null, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var userInfo = new User();

            try
            {
                userInfo.UserId = jsonResponse["id"].Value<string>();
                userInfo.Username = jsonResponse["username"].Value<string>();
                userInfo.Email = jsonResponse["email"].Value<string>();
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            return userInfo;
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
            var param = new Dictionary<string, object>();
            param["currency"] = "XBt"; // for Bitmex only currency is XBt

            string apiUrl = "/user/wallet";
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var wallet = new Wallet();

            try
            {
                wallet.Currency = jsonResponse["currency"].Value<string>();
                wallet.Amount = jsonResponse["amount"].Value<double>() / 100000000.0;
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }
            
            return wallet;
        }

        /// <summary>
        /// Request margin information of authenticated account for the currency of "XBt".
        /// </summary>
        /// <returns>Margin</returns>
        public Margin GetMargin()
            => GetMarginAsync().Result;

        /// <summary>
        /// <see cref="GetMargin()"/>
        /// </summary>
        public async Task<Margin> GetMarginAsync()
        {
            var param = new Dictionary<string, object>();
            param["currency"] = "XBt";

            string apiUrl = "/user/margin";
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var margin = new Margin();

            try
            {
                margin.Currency = jsonResponse["currency"].Value<string>();
                margin.Balance = jsonResponse["marginBalance"].Value<double>() / 100000000.0; // api returns in units of sathoshis
                margin.Available = jsonResponse["availableMargin"].Value<double>() / 100000000.0;
                margin.UnrealizedPnL = jsonResponse["unrealisedPnl"].Value<double>() / 100000000.0;
                margin.MarginLeverage = jsonResponse["marginLeverage"].Value<double>();
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            return margin;
        }

        /// <summary>
        /// Request open position of authenticated account for the given symbol.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <returns>a list of Position type</returns>
        public Position GetPosition(string symbol)
            => GetPositionAsync(symbol).Result;

        /// <summary>
        /// <see cref="GetPosition(string)"/>
        /// </summary>
        public async Task<Position> GetPositionAsync(string symbol)
        {
            var param = new Dictionary<string, object>();
            param["filter"] = "{ \"symbol\" : \"" + symbol + "\" }";

            string apiUrl = "/position";
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var position = new Position();

            JArray jArray = jsonResponse as JArray;
            if (jArray.Count == 0)
                return null; // user has no position at all

            // Api returns an array of all positions that matches with the given symbol string.
            // However, we are looking one cumulative position only for one symbol. 
            // So, if more than one position is returned then it is error. 

            if (jArray.Count > 1)
                throw new BitmexUnexpectedResponse(string.Format("Response of {0} should be returned only for one symbol", apiUrl));

            try
            {
                position.Symbol = jArray[0]["symbol"].Value<string>();
                position.Size = jArray[0]["currentQty"].Value<double>();
                position.Side = position.Size > 0 ? PositionType.Long
                    : position.Size < 0 ? PositionType.Short
                    : PositionType.Zero;
                position.AvgEntryPrice = position.Side == PositionType.Zero ? 0 : jArray[0]["avgEntryPrice"].Value<double>();
                position.LiquidationPrice = position.Side == PositionType.Zero ? 0 : jArray[0]["liquidationPrice"].Value<double>();
                position.UnrealizedPnL = position.Side == PositionType.Zero ? 0 : jArray[0]["unrealisedPnl"].Value<double>() / 100000000.0;
                position.Leverage = position.Side == PositionType.Zero ? 0 : jArray[0]["leverage"].Value<double>();
                position.MarginType = jArray[0]["crossMargin"].Value<bool>() ? MarginType.Cross : MarginType.Fixed;
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            // Bitmex returns one single cumulative position even user opens many.
            // So, this method should return a list of positions that contains only one element.

            return position;
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
            var param = new Dictionary<string, object>();
            param["symbol"] = symbol;

            string apiUrl = "/instrument";
            string response = await request.Query("GET", apiUrl, param, false).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var ticker = new Ticker();

            // Api returns an array of all symbols that matches with the given symbol string.
            // However, we are looking only for one symbol.

            JArray jArray = jsonResponse as JArray;
            if (jArray.Count == 0)
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));
            if (jArray.Count > 1)
                throw new BitmexUnexpectedResponse(string.Format("Response of {0} should be returned only for one symbol", apiUrl));

            try
            {
                ticker.Symbol = jArray[0]["symbol"].Value<string>();
                ticker.BaseCurrency = jArray[0]["settlCurrency"].Value<string>();
                ticker.QuoteCurrency = jArray[0]["quoteCurrency"].Value<string>();
                ticker.LastPrice = jArray[0]["lastPrice"].Value<double>();
                ticker.Timestamp = DateTime.SpecifyKind(jArray[0]["timestamp"].Value<DateTime>(), DateTimeKind.Utc);
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
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
            param["symbol"] = symbol;
            param["filter"] = "{ \"orderID\" : \"" + orderID + "\" }";

            string apiUrl = "/order";
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var order = new Order();

            // Api returns an array of all orders that matches with the given orderID string.
            // However, we are looking only for one order.

            JArray jArray = jsonResponse as JArray;
            if (jArray.Count == 0)
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));
            if (jArray.Count > 1)
                throw new BitmexUnexpectedResponse(string.Format("Response of {0} should be returned only for one orderID", apiUrl));

            try
            {
                string ordStatus = jArray[0]["ordStatus"].Value<string>();
                string ordType = jArray[0]["ordType"].Value<string>();
                string side = jArray[0]["side"].Value<string>();
                string execInst = jArray[0]["execInst"].Value<string>();

                order.Symbol = jArray[0]["symbol"].Value<string>();
                order.OrderID = jArray[0]["orderID"].Value<string>();
                order.Size = jArray[0]["orderQty"].Value<double>();
                order.FilledQuantity = jArray[0]["cumQty"].Value<double>();
                order.RemainingQuantity = jArray[0]["leavesQty"].Value<double>();

                order.Price = jArray[0]["price"].Type == JTokenType.Null ? 0
                    : jArray[0]["price"].Value<double>();

                order.TriggerPrice = jArray[0]["stopPx"].Type == JTokenType.Null ? 0
                    : jArray[0]["stopPx"].Value<double>();

                order.Side = side.Equals("Buy") ? OrderSide.Buy
                    : side.Equals("Sell") ? OrderSide.Sell
                    : throw new Exception();

                order.Type = ordType.Equals("Limit") && !execInst.Contains("Close") ? OrderType.Limit
                    : ordType.Equals("Market") && !execInst.Contains("Close") ? OrderType.Market
                    : ordType.Equals("Limit") && execInst.Contains("Close") ? OrderType.CloseLimit
                    : ordType.Equals("Market") && execInst.Contains("Close") ? OrderType.CloseMarket
                    : ordType.Equals("StopLimit") ? OrderType.StopLimit
                    : ordType.Equals("Stop") ? OrderType.StopMarket
                    : throw new Exception();

                order.Status = ordStatus.Equals("New") ? OrderStatus.Open
                    : ordStatus.Equals("Filled") ? OrderStatus.Filled
                    : ordStatus.Equals("Canceled") ? OrderStatus.Cancelled
                    : throw new Exception();
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
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
            param["symbol"] = symbol;
            param["count"] = max.ToString();
            param["filter"] = "{ \"open\" : true }";

            string apiUrl = "/order";
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            IList<Order> orderList = new List<Order>();

            JArray jArray = jsonResponse as JArray;
            if (jArray.Count == 0)
                return orderList; // user has not any open order 

            try
            {
                foreach (var jToken in jArray)
                {
                    var order = new Order();

                    string ordStatus = jToken["ordStatus"].Value<string>();
                    string ordType = jToken["ordType"].Value<string>();
                    string side = jToken["side"].Value<string>();
                    string execInst = jToken["execInst"].Value<string>();

                    order.Symbol = jToken["symbol"].Value<string>();
                    order.OrderID = jToken["orderID"].Value<string>();
                    order.Size = jToken["orderQty"].Value<double>();
                    order.FilledQuantity = jToken["cumQty"].Value<double>();
                    order.RemainingQuantity = jToken["leavesQty"].Value<double>();

                    order.Price = jToken["price"].Type == JTokenType.Null ? 0
                        : jToken["price"].Value<double>();

                    order.TriggerPrice = jToken["stopPx"].Type == JTokenType.Null ? 0
                        : jToken["stopPx"].Value<double>();

                    order.Side = side.Equals("Buy") ? OrderSide.Buy
                        : side.Equals("Sell") ? OrderSide.Sell
                        : throw new Exception();

                    order.Type = ordType.Equals("Limit") && !execInst.Contains("Close") ? OrderType.Limit
                        : ordType.Equals("Market") && !execInst.Contains("Close") ? OrderType.Market
                        : ordType.Equals("Limit") && execInst.Contains("Close") ? OrderType.CloseLimit
                        : ordType.Equals("Market") && execInst.Contains("Close") ? OrderType.CloseMarket
                        : ordType.Equals("StopLimit") ? OrderType.StopLimit
                        : ordType.Equals("Stop") ? OrderType.StopMarket
                        : throw new Exception();

                    order.Status = ordStatus.Equals("New") ? OrderStatus.Open
                        : ordStatus.Equals("Filled") ? OrderStatus.Filled
                        : ordStatus.Equals("Canceled") ? OrderStatus.Cancelled
                        : throw new Exception();

                    orderList.Add(order);
                }
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
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
            param["symbol"] = symbol;
            param["depth"] = depth.ToString();

            string apiUrl = "/orderBook/L2";
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            IList<RawOrder> orderBook = new List<RawOrder>();

            JArray jArray = jsonResponse as JArray;
            if (jArray.Count == 0)
                return orderBook;

            try
            {
                foreach (var jToken in jArray)
                {
                    var rawOrder = new RawOrder();

                    string side = jToken["side"].Value<string>();
                    rawOrder.Price = jToken["price"].Value<double>();
                    rawOrder.Size = jToken["size"].Value<double>();
                    rawOrder.Side = side.Equals("Buy") ? OrderSide.Buy
                        : side.Equals("Sell") ? OrderSide.Sell
                        : throw new Exception();

                    orderBook.Add(rawOrder);
                }
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
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
            param["symbol"] = symbol;
            param["startTime"] = start.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            param["endTime"] = end.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
            param["start"] = "0";
            param["count"] = "750";
            param["binSize"] = type == CandleType.OneMinute ? "1m"
                : type == CandleType.FiveMinutes ? "5m"
                : type == CandleType.OneHour ? "1h"
                : type == CandleType.OneDay ? "1d"
                : throw new BitmexUnexpectedArgument();

            string apiUrl = "/trade/bucketed";
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            IList<Candle> candles = new List<Candle>();

            JArray jArray = jsonResponse as JArray;
            if (jArray.Count == 0)
                return candles; // did not return any candle

            try
            {
                foreach (var jToken in jArray)
                {
                    var candle = new Candle();

                    candle.Symbol = jToken["symbol"].Value<string>();
                    candle.Timestamp = DateTime.SpecifyKind(jToken["timestamp"].Value<DateTime>(), DateTimeKind.Utc);
                    candle.Open = jToken["open"].Value<double>();
                    candle.High = jToken["high"].Value<double>();
                    candle.Low = jToken["low"].Value<double>();
                    candle.Close = jToken["close"].Value<double>();
                    candle.Volume = jToken["volume"].Value<double>();
                    candle.Type = type;

                    candles.Add(candle);
                }
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
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
        /// <param name="trigger">trigger price (only for stop orders)</param>
        /// <returns>orderID</returns>
        public string NewOrder(string symbol, OrderType type, double quantity, OrderSide? side = null, double? price = null, double? trigger = null)
            => NewOrderAsync(symbol, type, quantity, side, price, trigger).Result;

        /// <summary>
        /// <see cref="NewOrder(string, OrderType, double, OrderSide?, double?, double?)"/>
        /// </summary>
        public async Task<string> NewOrderAsync(string symbol, OrderType type, double quantity, OrderSide? side = null, double? price = null, double? trigger = null)
        {
            var param = new Dictionary<string, object>();

            param["symbol"] = symbol;

            param["ordType"] = type == OrderType.Limit ? "Limit"
                : type == OrderType.Market ? "Market"
                : type == OrderType.StopLimit ? "StopLimit"
                : type == OrderType.StopMarket ? "Stop"
                : type == OrderType.CloseLimit ? "Limit"
                : type == OrderType.CloseMarket ? "Market"
                : throw new BitmexUnexpectedArgument();

            param["orderQty"] = quantity.ToString();

            // You don't need to define "side" for close type orders
            if (side != null)
                param["side"] = side == OrderSide.Buy ? "Buy"
                    : side == OrderSide.Sell ? "Sell"
                    : throw new BitmexUnexpectedArgument();

            // Used only for limit type orders
            if (price != null)
                param["price"] = price.ToString();

            // Used only for stop type orders
            if (trigger != null)
                param["stopPx"] = trigger.ToString();

            // Used only for close type orders
            if (type == OrderType.CloseLimit || type == OrderType.CloseMarket)
                param["execInst"] = "Close";

            string apiUrl = "/order";
            string response = await request.Query("POST", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            try
            {
                return jsonResponse["orderID"].Value<string>();
            }
            catch
            {
                throw new BitmexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }
        }

        /// <summary>
        /// Amend an existing order with orderID.
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <param name="quantity">order quantity</param>
        /// <param name="price">price (only for limit orders)</param>
        /// <param name="trigger">trigger price (only for stop orders)</param>
        public void AmendOrder(string orderID, double quantity, double? price = null, double? trigger = null)
            => AmendOrderAsync(orderID, quantity, price, trigger).Wait();

        /// <summary>
        /// <see cref="AmendOrder(string, double, double?, double?)"/>
        /// </summary>
        public async Task AmendOrderAsync(string orderID, double quantity, double? price = null, double? trigger = null)
        {
            var param = new Dictionary<string, object>();
            param["orderID"] = orderID;
            param["orderQty"] = quantity.ToString();

            if (price != 0) // used only for limit orders
                param["price"] = price.ToString();

            if (trigger != 0) // used only for stop orders
                param["stopPx"] = trigger.ToString();

            string apiUrl = "/order";
            string response = await request.Query("PUT", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);
        }

        /// <summary>
        /// Cancel an existing order with orderID.
        /// </summary>
        /// <param name="orderID">orderID</param>
        public void CancelOrder(string orderID)
            => CancelOrderAsync(orderID).Wait();

        /// <summary>
        /// <see cref="CancelOrder(string)"/>
        /// </summary>
        public async Task CancelOrderAsync(string orderID)
        {
            var param = new Dictionary<string, object>();
            param["orderID"] = orderID;

            string apiUrl = "/order";
            string response = await request.Query("DELETE", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);
        }

        /// <summary>
        /// Cancel all orders for the given symbol of trading.
        /// </summary>
        /// <param name="symbol">symbol</param>
        public void CancelAllOrders(string symbol = null)
            => CancelAllOrdersAsync(symbol).Wait();

        /// <summary>
        /// <see cref="CancelAllOrders(string)"/>
        /// </summary>
        public async Task CancelAllOrdersAsync(string symbol = null)
        {
            var param = new Dictionary<string, object>();
            param["symbol"] = symbol;

            string apiUrl = "/order/all";
            string response = await request.Query("DELETE", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);
        }

        /// <summary>
        /// Set leverage and margin options for the given currency and/or symbol. 
        /// </summary>
        /// <param name="symbol">Symbol name, i.e. "XBTUSD"</param>
        /// <param name="margin">Margin type, i.e., Cross of Fixed (isolated)</param>
        /// <param name="leverage">If margin type is set to Cross then it is dummy.</param>
        public void SetLeverage(string symbol, MarginType margin, double leverage)
            => SetLeverageAsync(symbol, margin, leverage).Wait();

        /// <summary>
        /// <see cref="SetLeverage(string, MarginType, double)"/>
        /// </summary>
        public async Task SetLeverageAsync(string symbol, MarginType margin, double leverage)
        {
            var param = new Dictionary<string, object>();
            param["symbol"] = symbol;
            param["leverage"] = margin == MarginType.Cross ? "0" : leverage.ToString();

            string apiUrl = "/position/leverage";
            string response = await request.Query("POST", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new BitmexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);
        }

        #endregion

        #region Private Methods

        /* This method checks if response contains any error such that api returns 
         * If the response contains an error then regarding information is encapsulated and ExchangeAPIError is thrown 
         */
        private void ValidateResponse(string apiurl, JToken token)
        {
            if (!(token is JArray) && token["error"] != null)
                throw new BitmexAPIError(string.Format("Request to {0} returned error", apiurl),
                    null,
                    token["error"]["name"].ToString(),
                    token["error"]["message"].ToString());
        }

        #endregion
    }
}

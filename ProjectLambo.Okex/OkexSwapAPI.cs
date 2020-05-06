using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectLambo.Okex.Models;

namespace ProjectLambo.Okex
{
    /// <summary>
    /// This class implements Okex v3 API endpoints for swap contracts.
    /// </summary>
    public class OkexSwapAPI
    {
        #region Fields

        private readonly OkexWebRequest request;

        #endregion

        #region Constructers

        /// <summary>
        /// Default constructer.
        /// </summary>
        public OkexSwapAPI()
        {
            request = new OkexWebRequest("/api/swap/v3");
        }

        /// <summary>
        /// Constructer.
        /// </summary>
        /// <param name="apiKey">key</param>
        /// <param name="apiSecret">secret</param>
        /// <param name="apiPassphrase">passphrase</param>
        public OkexSwapAPI(string apiKey, string apiSecret, string apiPassphrase)
        {
            request = new OkexWebRequest("/api/swap/v3", apiKey, apiSecret, apiPassphrase);
        }

        /// <summary>
        /// Constructer with dependency injection for unit test project.
        /// </summary>
        /// <param name="request"></param>
        internal OkexSwapAPI(OkexWebRequest request)
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
        /// Request margin information of authenticated account for the given symbol.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <returns>Margin</returns>
        public Margin GetMargin(string symbol)
            => GetMarginAsync(symbol).Result;

        /// <summary>
        /// <see cref="GetMargin(string)"/>
        /// </summary>
        /// <param name="symbol">symbol, i.e. "BTC-USD-SWAP"</param>
        public async Task<Margin> GetMarginAsync(string symbol)
        {
            string apiUrl = "/" + symbol + "/accounts";
            string response = await request.Query("GET", apiUrl, null, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var margin = new Margin();

            try
            {
                margin.Symbol = jsonResponse["info"]["instrument_id"].Value<string>();
                margin.Balance = jsonResponse["info"]["equity"].Value<double>();
                margin.Available = jsonResponse["info"]["total_avail_balance"].Value<double>();
                margin.UnrealizedPnL = jsonResponse["info"]["unrealized_pnl"].Value<double>();
                margin.MarginLeverage = jsonResponse["info"]["margin_ratio"].Value<double>();
                margin.MarginType = jsonResponse["info"]["margin_mode"].Value<string>() == "crossed" ? MarginType.Cross : MarginType.Fixed;
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            return margin;
        }

        /// <summary>
        /// Request open positions of authenticated account for the given symbol.
        /// </summary>
        /// <param name="symbol">symbol</param>
        /// <returns>a list of Position type</returns>
        public IList<Position> GetPositions(string symbol)
            => GetPositionsAsync(symbol).Result;

        /// <summary>
        /// <see cref="GetPositions(string)"/>
        /// </summary>
        /// <param name="symbol">symbol, i.e. "BTC-USD-SWAP"</param>
        public async Task<IList<Position>> GetPositionsAsync(string symbol)
        {
            string apiUrl = "/" + symbol + "/position";
            string response = await request.Query("GET", apiUrl, null, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var positions = new List<Position>();

            var holding = jsonResponse["holding"] as JArray;
            if (holding.Count == 0)
                return positions; // user has no position at all

            try
            {
                for (int i = 0; i < holding.Count; i++)
                {
                    var position = new Position();

                    string side = holding[i]["side"].Value<string>();
                    position.Symbol = symbol;
                    position.Size = holding[i]["avail_position"].Value<double>() * 100; // each okex swap contract values 100 USD
                    position.Side = side == "long" ? PositionType.Long
                        : side == "short" ? PositionType.Short
                        : throw new Exception();
                    position.AvgEntryPrice = holding[i]["avg_cost"].Value<double>();
                    position.LiquidationPrice = holding[i]["liquidation_price"].Value<double>();
                    position.Leverage = holding[i]["leverage"].Value<double>();
                    position.MarginType = jsonResponse["margin_mode"].Value<string>() == "crossed" ? MarginType.Cross : MarginType.Fixed;

                    // I assume short positions have negative sizes (for compatibility with Bitmex)
                    if (position.Side == PositionType.Short)
                        position.Size *= -1;

                    positions.Add(position);
                }
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            return positions;
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
                ticker.BaseCurrency = jsonResponse["instrument_id"].Value<string>().Split('-')[0];
                ticker.QuoteCurrency = jsonResponse["instrument_id"].Value<string>().Split('-')[1];
                ticker.LastPrice = jsonResponse["last"].Value<double>();
                ticker.Timestamp = DateTime.SpecifyKind(jsonResponse["timestamp"].Value<DateTime>(), DateTimeKind.Utc);
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
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
            string apiUrl = "/orders/" + symbol + "/" + orderID;
            string response = await request.Query("GET", apiUrl, null, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var order = new Order();

            try
            {
                string ordStatus = jsonResponse["state"].Value<string>();
                string ordType = jsonResponse["type"].Value<string>();

                order.Symbol = jsonResponse["instrument_id"].Value<string>();
                order.OrderID = jsonResponse["order_id"].Value<string>();
                order.Size = jsonResponse["size"].Value<double>();
                order.FilledQuantity = jsonResponse["filled_qty"].Value<double>();
                order.RemainingQuantity = order.Size - order.FilledQuantity;

                order.Price = jsonResponse["price"] == null ? 0
                    : jsonResponse["price"].Type == JTokenType.Null ? 0
                    : jsonResponse["price"].Value<double>();

                order.Side = ordType.Equals("1") ? OrderSide.Buy
                    : ordType.Equals("2") ? OrderSide.Sell
                    : ordType.Equals("3") ? OrderSide.Sell
                    : ordType.Equals("4") ? OrderSide.Buy
                    : throw new Exception();

                order.Type = OrderType.Limit; // Okex swap contracts only support limit orders

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
            param["state"] = "6";

            string apiUrl = "/orders/" + symbol;
            string response = await request.Query("GET", apiUrl, param, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);

            var openOrders = new List<Order>();

            var jArray = jsonResponse["order_info"] as JArray;
            if (jArray.Count == 0)
                return openOrders; // did not return any order

            try
            {
                foreach (JToken token in jArray)
                {
                    var order = new Order();

                    string ordStatus = token["state"].Value<string>();
                    string ordType = token["type"].Value<string>();

                    order.Symbol = token["instrument_id"].Value<string>();
                    order.OrderID = token["order_id"].Value<string>();
                    order.Size = token["size"].Value<double>();
                    order.FilledQuantity = token["filled_qty"].Value<double>();
                    order.RemainingQuantity = order.Size - order.FilledQuantity;

                    order.Price = token["price"] == null ? 0
                        : token["price"].Type == JTokenType.Null ? 0
                        : token["price"].Value<double>();

                    order.Side = ordType.Equals("1") ? OrderSide.Buy
                        : ordType.Equals("2") ? OrderSide.Sell
                        : ordType.Equals("3") ? OrderSide.Sell
                        : ordType.Equals("4") ? OrderSide.Buy
                        : throw new Exception();

                    order.Type = OrderType.Limit; // Okex swap contracts only support limit orders

                    // For simplicity, order status is assumed to be in one of three states: Open, Cancelled, Filled
                    order.Status = ordStatus.Equals("-2") ? OrderStatus.Cancelled
                        : ordStatus.Equals("-1") ? OrderStatus.Cancelled
                        : ordStatus.Equals("0") ? OrderStatus.Open
                        : ordStatus.Equals("1") ? OrderStatus.Open
                        : ordStatus.Equals("2") ? OrderStatus.Filled
                        : ordStatus.Equals("3") ? OrderStatus.Open
                        : ordStatus.Equals("4") ? OrderStatus.Cancelled
                        : throw new Exception();

                    openOrders.Add(order);
                }
            }
            catch
            {
                throw new OkexResponseParseError(string.Format("Response of {0} couldn't be parsed as expected", apiUrl));
            }

            return openOrders;
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

            string apiUrl = "/instruments/" + symbol + "/depth";
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
                    candle.Type = type;

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
        /// <param name="price">price</param>
        /// <returns>orderID</returns>
        public string NewOrder(string symbol, OrderType type, double quantity, OrderSide side, double price)
            => NewOrderAsync(symbol, type, quantity, side, price).Result;

        /// <summary>
        /// <see cref="NewOrder(string, OrderType, double, OrderSide, double)"/>
        /// </summary>
        public async Task<string> NewOrderAsync(string symbol, OrderType type, double quantity, OrderSide side, double price)
        {
            var param = new Dictionary<string, object>();

            param["instrument_id"] = symbol;

            // Okex swap api accepts only limit and close type orders
            param["type"] = (type == OrderType.Limit && side == OrderSide.Buy) ? "1" // open long
                : (type == OrderType.Limit && side == OrderSide.Sell) ? "2" // open short
                : (type == OrderType.CloseLimit && side == OrderSide.Sell) ? "3" // close long
                : (type == OrderType.CloseLimit && side == OrderSide.Buy) ? "4" // close short
                : throw new OkexUnexpectedArgument();

            param["size"] = (quantity / 100).ToString(); // A contract in Okex values 100 USD

            param["price"] = price.ToString();

            string apiUrl = "/order";
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
            string apiUrl = "/cancel_order/" + symbol + "/" + orderID;
            string response = await request.Query("POST", apiUrl, null, true).ConfigureAwait(false);
            if (string.IsNullOrEmpty(response))
                throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

            JToken jsonResponse = JsonConvert.DeserializeObject<JToken>(response);
            ValidateResponse(apiUrl, jsonResponse);
        }

        /// <summary>
        /// Set leverage and margin options for the given currency and/or symbol. 
        /// </summary>
        /// <param name="symbol">Symbol name, i.e. "BTC-USD-SWAP"</param>
        /// <param name="margin">Margin type, i.e., Cross of Fixed (isolated)</param>
        /// <param name="leverage">Leverage ratio</param>
        public void SetLeverage(string symbol, MarginType margin, double leverage)
            => SetLeverageAsync(symbol, margin, leverage).Wait();

        /// <summary>
        /// <see cref="SetLeverage(string, MarginType, double)"/>
        /// </summary>
        public async Task SetLeverageAsync(string symbol, MarginType margin, double leverage)
        {
            var param = new Dictionary<string, object>();
            string apiUrl = "/accounts/" + symbol + "/leverage";
            string response;

            if (margin == MarginType.Cross)
            {
                param["leverage"] = leverage.ToString();
                param["side"] = "3";

                response = await request.Query("POST", apiUrl, param, true).ConfigureAwait(false);
                if (string.IsNullOrEmpty(response))
                    throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

                ValidateResponse(apiUrl, JsonConvert.DeserializeObject<JToken>(response));
            }
            else // fixed margin
            {
                // set long leverage

                param["leverage"] = leverage.ToString();
                param["side"] = "1"; // fixed long

                response = await request.Query("POST", apiUrl, param, true).ConfigureAwait(false);
                if (string.IsNullOrEmpty(response))
                    throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

                ValidateResponse(apiUrl, JsonConvert.DeserializeObject<JToken>(response));

                // set short leverage

                param.Clear();
                param["leverage"] = leverage.ToString();
                param["side"] = "2"; // fixed short

                response = await request.Query("POST", apiUrl, param, true).ConfigureAwait(false);
                if (string.IsNullOrEmpty(response))
                    throw new OkexNoResponse(string.Format("Request to {0} returned empty", apiUrl));

                ValidateResponse(apiUrl, JsonConvert.DeserializeObject<JToken>(response));
            }
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
                if (token["code"] != null)
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
                if (token["error_code"] != null)
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

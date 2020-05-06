using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectLambo.Bitmex;
using ProjectLambo.Bitmex.Models;

namespace ProjectLambo.Tests.Bitmex
{
    [TestClass]
    public class BitmexAPITests
    {
        [TestMethod]
        public virtual void GetUser_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            User user = exchange.GetUser();

            // Assert
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public virtual void GetUser_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.GetUser(); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetWallet_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            Wallet wallet = exchange.GetWallet();

            // Assert
            Assert.IsNotNull(wallet);
        }

        [TestMethod]
        public virtual void GetWallet_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.GetWallet(); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetMargin_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            Margin margin = exchange.GetMargin();

            // Assert
            Assert.IsNotNull(margin);
        }

        [TestMethod]
        public virtual void GetMargin_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.GetMargin(); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetPosition_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            Position position = exchange.GetPosition("XBTUSD");

            // Assert
            Assert.IsNotNull(position);
        }

        [TestMethod]
        public virtual void GetPosition_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.GetPosition("XBTUSD"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetPosition_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.GetPosition(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetTicker_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            Ticker ticker = exchange.GetTicker("XBTUSD");

            // Assert
            Assert.IsNotNull(ticker);
        }

        [TestMethod]
        public virtual void GetTicker_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.GetTicker(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrder_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            Order order = exchange.GetOrder("XBTUSD", "some_order_id");

            // Assert
            Assert.IsNotNull(order);
        }

        [TestMethod]
        public virtual void GetOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.GetOrder("XBTUSD", "some_order_id"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrder_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.GetOrder(null, "some_order_id"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrder_BadOrderId_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.GetOrder("XBTUSD", null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOpenOrders_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            IList<Order> openOrders = exchange.GetOpenOrders("XBTUSD");

            // Assert
            Assert.IsNotNull(openOrders);
        }

        [TestMethod]
        public virtual void GetOpenOrders_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.GetOpenOrders("XBTUSD"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOpenOrders_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.GetOpenOrders(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrderBook_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            IList<RawOrder> orderBook = exchange.GetOrderBook("XBTUSD");

            // Assert
            Assert.IsNotNull(orderBook);
        }

        [TestMethod]
        public virtual void GetOrderBook_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.GetOrderBook("XBTUSD"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrderBook_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.GetOrderBook(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetCandlesticks_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            IList<Candle> candles = exchange.GetCandlesticks("XBTUSD", CandleType.OneHour, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now);

            // Assert
            Assert.IsNotNull(candles);
        }

        [TestMethod]
        public virtual void GetCandlesticks_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.GetCandlesticks(null, CandleType.OneHour, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_LimitBuy_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.Limit, 100, OrderSide.Buy, 10000, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_LimitSell_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.Limit, 100, OrderSide.Sell, 10000, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_MarketBuy_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.Market, 100, OrderSide.Buy, null, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_MarketSell_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.Market, 100, OrderSide.Sell, null, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_StopLimitBuy_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.StopLimit, 100, OrderSide.Buy, 10000, 10100);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_StopLimitSell_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.StopLimit, 100, OrderSide.Sell, 10000, 9900);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_StopMarketBuy_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.StopMarket, 100, OrderSide.Buy, null, 10000);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_StopMarketSell_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.StopMarket, 100, OrderSide.Sell, null, 10000);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_CloseLimitBuy_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.CloseLimit, 100, OrderSide.Buy, 10000, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_CloseLimitSell_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.CloseLimit, 100, OrderSide.Sell, 10000, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_CloseMarketBuy_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.CloseMarket, 100, OrderSide.Buy, null, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_CloseMarketSell_ShouldSuccess()
        {
            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            string orderID = exchange.NewOrder("XBTUSD", OrderType.CloseMarket, 100, OrderSide.Sell, null, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.Market, 100, OrderSide.Buy, null, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.NewOrder(null, OrderType.Market, 100, OrderSide.Buy, null, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_InsufficientBalance_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.Market, 999999, OrderSide.Buy, null, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoSideForLimitOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.Limit, 100, null, 10000, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoSideForMarketOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.Market, 100, null, null, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoPriceForLimitOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.Limit, 100, OrderSide.Buy, null, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoSideForStopLimitOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.StopLimit, 100, null, 10000, 10100); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoSideForStopMarketOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.StopMarket, 100, null, null, 10000); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoSideForCloseLimitOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.CloseLimit, 100, null, 10000, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoSideForCloseMarketOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.CloseMarket, 100, null, null, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoPriceForStopLimitOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.StopLimit, 100, OrderSide.Buy, null, 10000); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoPriceForCloseLimitOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.CloseLimit, 100, OrderSide.Buy, null, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoTriggerForStopLimitOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.StopLimit, 100, OrderSide.Buy, 10000, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoTriggerForStopMarketOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("XBTUSD", OrderType.StopMarket, 100, OrderSide.Buy, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void AmendOrder_ShouldSuccess()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.AmendOrder("some_order_id", 200); } catch { exceptionThrown = true; }

            // Assert
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public virtual void AmendOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.AmendOrder("some_order_id", 200); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void AmendOrder_BadOrderId_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.AmendOrder(null, 200); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_ShouldSuccess()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.CancelOrder("some_order_id"); } catch { exceptionThrown = true; }

            // Assert
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.CancelOrder("some_order_id"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_BadOrderId_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.CancelOrder(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelAllOrders_ShouldSuccess()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.CancelAllOrders("XBTUSD"); } catch { exceptionThrown = true; }

            // Assert
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelAllOrders_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.CancelAllOrders("XBTUSD"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelAllOrders_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.CancelAllOrders(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void SetLeverage_ShouldSuccess()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.SetLeverage("XBTUSD", MarginType.Fixed, 10); } catch { exceptionThrown = true; }

            // Assert
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public virtual void SetLeverage_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());

            // Act (one single action only)
            try { exchange.SetLeverage("XBTUSD", MarginType.Fixed, 10); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void SetLeverage_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            BitmexAPI exchange = new BitmexAPI(new BitmexAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret");

            // Act (one single action only)
            try { exchange.SetLeverage(null, MarginType.Fixed, 10); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }
    }
}

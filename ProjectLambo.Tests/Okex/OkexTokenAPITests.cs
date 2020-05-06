using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectLambo.Okex;
using ProjectLambo.Okex.Models;

namespace ProjectLambo.Tests.Okex
{
    [TestClass]
    public class OkexTokenAPITests
    {
        [TestMethod]
        public virtual void GetWallet_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

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
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            try { exchange.GetWallet(); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetTicker_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            Ticker ticker = exchange.GetTicker("BTC-USDT");

            // Assert
            Assert.IsNotNull(ticker);
        }

        [TestMethod]
        public virtual void GetTicker_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            try { exchange.GetTicker(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrder_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            Order order = exchange.GetOrder("BTC-USDT", "some_order_id");

            // Assert
            Assert.IsNotNull(order);
        }

        [TestMethod]
        public virtual void GetOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            try { exchange.GetOrder("BTC-USDT", "some_order_id"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrder_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

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
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.GetOrder("BTC-USDT", null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOpenOrders_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            IList<Order> openOrders = exchange.GetOpenOrders("BTC-USDT");

            // Assert
            Assert.IsNotNull(openOrders);
        }

        [TestMethod]
        public virtual void GetOpenOrders_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            try { exchange.GetOpenOrders("BTC-USDT"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOpenOrders_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.GetOpenOrders(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrderBook_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            IList<RawOrder> orderBook = exchange.GetOrderBook("BTC-USDT");

            // Assert
            Assert.IsNotNull(orderBook);
        }

        [TestMethod]
        public virtual void GetOrderBook_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            try { exchange.GetOrderBook("BTC-USDT"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrderBook_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.GetOrderBook(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetCandlesticks_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            IList<Candle> candles = exchange.GetCandlesticks("BTC-USDT", CandleType.OneHour, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now);

            // Assert
            Assert.IsNotNull(candles);
        }

        [TestMethod]
        public virtual void GetCandlesticks_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            try { exchange.GetCandlesticks(null, CandleType.OneHour, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_LimitBuy_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            string orderID = exchange.NewOrder("BTC-USDT", OrderType.Limit, 100, OrderSide.Buy, 10000);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_LimitSell_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            string orderID = exchange.NewOrder("BTC-USDT", OrderType.Limit, 100, OrderSide.Sell, 10000);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_MarketBuy_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            string orderID = exchange.NewOrder("BTC-USDT", OrderType.Market, 100, OrderSide.Buy, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_MarketSell_ShouldSuccess()
        {
            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            string orderID = exchange.NewOrder("BTC-USDT", OrderType.Market, 100, OrderSide.Sell, null);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("BTC-USDT", OrderType.Market, 100, OrderSide.Buy, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.NewOrder(null, OrderType.Market, 100, OrderSide.Buy, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_InsufficientBalance_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.NewOrder("BTC-USDT", OrderType.Market, 999999, OrderSide.Buy, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_NoPriceForLimitOrders_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.NewOrder("BTC-USDT", OrderType.Limit, 100, OrderSide.Buy, null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_ShouldSuccess()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.CancelOrder("BTC-USDT", "some_order_id"); } catch { exceptionThrown = true; }

            // Assert
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());

            // Act (one single action only)
            try { exchange.CancelOrder("BTC-USDT", "some_order_id"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.CancelOrder(null, "some_order_id"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_BadOrderId_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexTokenAPI exchange = new OkexTokenAPI(new OkexTokenAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.CancelOrder("BTC-USDT", null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }
    }
}

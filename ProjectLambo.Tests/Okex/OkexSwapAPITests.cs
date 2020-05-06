using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectLambo.Okex;
using ProjectLambo.Okex.Models;

namespace ProjectLambo.Tests.Okex
{
    [TestClass]
    public class OkexSwapAPITests
    {
        [TestMethod]
        public virtual void GetMargin_ShouldSuccess()
        {
            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            Margin margin = exchange.GetMargin("BTC-USD-SWAP");

            // Assert
            Assert.IsNotNull(margin);
        }

        [TestMethod]
        public virtual void GetMargin_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.GetMargin("BTC-USD-SWAP"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetMargin_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.GetMargin(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetPosition_ShouldSuccess()
        {
            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            IList<Position> position = exchange.GetPositions("BTC-USD-SWAP");

            // Assert
            Assert.IsNotNull(position);
        }

        [TestMethod]
        public virtual void GetPosition_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.GetPositions("BTC-USD-SWAP"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetPosition_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.GetPositions(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetTicker_ShouldSuccess()
        {
            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            Ticker ticker = exchange.GetTicker("BTC-USD-SWAP");

            // Assert
            Assert.IsNotNull(ticker);
        }

        [TestMethod]
        public virtual void GetTicker_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.GetTicker(null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrder_ShouldSuccess()
        {
            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            Order order = exchange.GetOrder("BTC-USD-SWAP", "some_order_id");

            // Assert
            Assert.IsNotNull(order);
        }

        [TestMethod]
        public virtual void GetOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.GetOrder("BTC-USD-SWAP", "some_order_id"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrder_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
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
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.GetOrder("BTC-USD-SWAP", null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOpenOrders_ShouldSuccess()
        {
            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            IList<Order> openOrders = exchange.GetOpenOrders("BTC-USD-SWAP");

            // Assert
            Assert.IsNotNull(openOrders);
        }

        [TestMethod]
        public virtual void GetOpenOrders_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.GetOpenOrders("BTC-USD-SWAP"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOpenOrders_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
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
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            IList<RawOrder> orderBook = exchange.GetOrderBook("BTC-USD-SWAP");

            // Assert
            Assert.IsNotNull(orderBook);
        }

        [TestMethod]
        public virtual void GetOrderBook_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.GetOrderBook("BTC-USD-SWAP"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void GetOrderBook_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
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
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            IList<Candle> candles = exchange.GetCandlesticks("BTC-USD-SWAP", CandleType.OneHour, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now);

            // Assert
            Assert.IsNotNull(candles);
        }

        [TestMethod]
        public virtual void GetCandlesticks_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.GetCandlesticks(null, CandleType.OneHour, DateTime.Now.Subtract(TimeSpan.FromDays(1)), DateTime.Now); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_LimitBuy_ShouldSuccess()
        {
            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            string orderID = exchange.NewOrder("BTC-USD-SWAP", OrderType.Limit, 100, OrderSide.Buy, 10000);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_LimitSell_ShouldSuccess()
        {
            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            string orderID = exchange.NewOrder("BTC-USD-SWAP", OrderType.Limit, 100, OrderSide.Sell, 10000);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_CloseLimitBuy_ShouldSuccess()
        {
            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            string orderID = exchange.NewOrder("BTC-USD-SWAP", OrderType.CloseLimit, 100, OrderSide.Buy, 10000);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_CloseLimitSell_ShouldSuccess()
        {
            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            string orderID = exchange.NewOrder("BTC-USD-SWAP", OrderType.CloseLimit, 100, OrderSide.Sell, 10000);

            // Assert
            Assert.IsNotNull(orderID);
        }

        [TestMethod]
        public virtual void NewOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.NewOrder("BTC-USD-SWAP", OrderType.Limit, 100, OrderSide.Buy, 10000); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.NewOrder(null, OrderType.Limit, 100, OrderSide.Buy, 10000); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void NewOrder_InsufficientBalance_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.NewOrder("BTC-USD-SWAP", OrderType.Limit, 999999, OrderSide.Buy, 10000); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_ShouldSuccess()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.CancelOrder("BTC-USD-SWAP", "some_order_id"); } catch { exceptionThrown = true; }

            // Assert
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.CancelOrder("BTC-USD-SWAP", "some_order_id"); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void CancelOrder_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
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
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.CancelOrder("BTC-USD-SWAP", null); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void SetLeverage_ShouldSuccess()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.SetLeverage("BTC-USD-SWAP", MarginType.Fixed, 10); } catch { exceptionThrown = true; }

            // Assert
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public virtual void SetLeverage_NotAuthenticated_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());

            // Act (one single action only)
            try { exchange.SetLeverage("BTC-USD-SWAP", MarginType.Fixed, 10); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public virtual void SetLeverage_BadSymbol_ShouldThrowException()
        {
            bool exceptionThrown = false;

            // Arrange
            OkexSwapAPI exchange = new OkexSwapAPI(new OkexSwapAPIStub());
            exchange.SetKeys("some_dummy_key", "some_dummy_secret", "some_dummy_passphrase");

            // Act (one single action only)
            try { exchange.SetLeverage(null, MarginType.Fixed, 10); } catch (Exception) { exceptionThrown = true; }

            // Assert
            Assert.IsTrue(exceptionThrown);
        }
    }
}

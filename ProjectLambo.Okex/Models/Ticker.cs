using System;

namespace ProjectLambo.Okex.Models
{
    /// <summary>
    /// Class definition for holding information regarding the trading symbol.
    /// </summary>
    public class Ticker
    {
        public string Symbol { get; internal set; }

        public string BaseCurrency { get; internal set; } // i.e., BTC for trading BTCUSD symbol

        public string QuoteCurrency { get; internal set; } // i.e., USD for trading BTCUSD symbol

        public double? LastPrice { get; internal set; } // In units of quote currency

        public DateTime? Timestamp { get; internal set; }
    }
}

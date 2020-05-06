using System;

namespace ProjectLambo.Bitmex.Models
{
    /// <summary>
    /// Class definition to store candlestick data.
    /// </summary>
    public class Candle
    {
        public string Symbol { get; internal set; }

        public CandleType? Type { get; internal set; }
        
        public DateTime? Timestamp { get; internal set; } // GMT +0 time

        public double? Open { get; internal set; }

        public double? High { get; internal set; }

        public double? Low { get; internal set; }

        public double? Close { get; internal set; }
        
        public double? Volume { get; internal set; }

        public TimeSpan TimeSpan
        {
            get
            {
                switch (Type)
                {
                    case CandleType.OneMinute:
                        return TimeSpan.FromMinutes(1);

                    case CandleType.FiveMinutes:
                        return TimeSpan.FromMinutes(5);

                    case CandleType.OneHour:
                        return TimeSpan.FromHours(1);

                    case CandleType.OneDay:
                        return TimeSpan.FromDays(1);

                    default:
                        return TimeSpan.MaxValue;
                }
            }
        }
    }
}

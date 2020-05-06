namespace ProjectLambo.Bitmex.Models
{
    /// <summary>
    /// Class definition to store position information of authenticated account.
    /// </summary>
    public class Position
    {
        public string Symbol { get; internal set; }

        public PositionType? Side { get; internal set; }
        
        public double? Size { get; internal set; }

        public double? AvgEntryPrice { get; internal set; }

        public double? LiquidationPrice { get; internal set; }

        public double? UnrealizedPnL { get; internal set; }

        public double? Leverage { get; internal set; }

        public MarginType? MarginType { get; internal set; }
    }
}

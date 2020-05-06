namespace ProjectLambo.Okex.Models
{
    /// <summary>
    /// Class definition to store margin information of authenticated account.
    /// </summary>
    public class Margin
    {
        public string Symbol { get; internal set; }

        public double? Balance { get; internal set; }

        public double? Available { get; internal set; }

        public double? UnrealizedPnL { get; internal set; }

        public double? MarginLeverage { get; internal set; }

        public MarginType? MarginType { get; internal set; }
    }
}

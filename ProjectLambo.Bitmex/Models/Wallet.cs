namespace ProjectLambo.Bitmex.Models
{
    /// <summary>
    /// Class definition to store wallet information of authenticated account.
    /// </summary>
    public class Wallet
    {
        public string Currency { get; internal set; }

        public double? Amount { get; internal set; }
    }
}

namespace ProjectLambo.Okex.Models
{
    /// <summary>
    /// Class definition to store orders of authenticated account.
    /// <seealso cref="RawOrder"/>
    /// </summary>
    public class Order : RawOrder
    {
        public string Symbol { get; internal set; }

        public string OrderID { get; internal set; }

        public OrderType? Type { get; internal set; }

        public OrderStatus? Status { get; internal set; }

        public double? FilledQuantity { get; internal set; }

        public double? RemainingQuantity { get; internal set; }
    }
}

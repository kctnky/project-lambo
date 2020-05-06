namespace ProjectLambo.Okex.Models
{
    /// <summary>
    /// Class definition for basic order.
    /// </summary>
    public class RawOrder
    {
        public OrderSide? Side { get; internal set; }

        public double? Size { get; internal set; }

        public double? Price { get; internal set; }
    }
}

using System.Collections.Generic;

namespace ProjectLambo.Okex.Models
{
    /// <summary>
    /// Class definition for holding amount of a single element.
    /// </summary>
    public class Holding
    {
        public string Currency { get; internal set; }

        public double? Balance { get; internal set; }

        public double? Available { get; internal set; }
    }

    /// <summary>
    /// Class definition to store wallet information of authenticated account.
    /// </summary>
    public class Wallet
    {
        public IList<Holding> Currencies { get; internal set; } = new List<Holding>();

        //public double? BtcEquivalent { get; internal set; }

        //public double? UsdEquivalent { get; internal set; }

        public Holding this[int index]
        {
            get { return Currencies[index]; }
        }

        public Holding this[string currency]
        {
            get
            {
                foreach (var c in Currencies)
                    if (c.Currency == currency)
                        return c;
                return null;
            }
        }
    }
}

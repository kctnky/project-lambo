namespace ProjectLambo.Okex
{
    public enum OrderSide
    {
        Buy,
        Sell
    }

    public enum OrderType
    {
        Limit,
        Market,
        CloseLimit,
        CloseMarket
    }

    public enum OrderStatus
    {
        Open,
        Filled,
        Cancelled
    }

    public enum CandleType
    {
        OneMinute,
        FiveMinutes,
        FifteenMinutes,
        ThirtyMinutes,
        OneHour,
        TwoHours,
        FourHours,
        OneDay
    }

    public enum MarginType
    {
        Fixed,
        Cross
    }

    public enum PositionType
    {
        Zero,
        Long,
        Short
    }
}

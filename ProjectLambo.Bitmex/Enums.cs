namespace ProjectLambo.Bitmex
{
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

    public enum OrderSide
    {
        Buy,
        Sell
    }

    public enum OrderType
    {
        Limit,
        Market,
        StopLimit,
        StopMarket,
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
        None,
        OneMinute,
        FiveMinutes,
        OneHour,
        OneDay
    }
}

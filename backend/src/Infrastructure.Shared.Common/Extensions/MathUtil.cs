namespace Infrastructure.Shared.Common.Extensions;

public static class MathUtil
{
    public static long RoundToCents(decimal moneyWithCentsAsDecimals)
    {
        return (long)Math.Round(moneyWithCentsAsDecimals * 100, 0, MidpointRounding.ToEven);
    }
}
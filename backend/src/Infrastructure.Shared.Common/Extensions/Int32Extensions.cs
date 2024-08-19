using System.Globalization;

namespace Infrastructure.Shared.Common.Extensions;

public static class Int32Extensions
{
    public static double GetDouble(this int value)
    {
        return Convert.ToDouble($"{value / 100:0}.{value % 100:00}", CultureInfo.InvariantCulture);
    }

    public static decimal GetDecimal(this int value)
    {
        return Convert.ToDecimal($"{value / 100:0}.{value % 100:00}", CultureInfo.InvariantCulture);
    }

    public static string GetCurrency(this int value)
    {
        return $"R$ {value / 100:0}.{value % 100:00}";
    }
}
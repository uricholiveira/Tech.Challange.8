using System.Globalization;

namespace Infrastructure.Shared.Common.Extensions;

public static class UnsignedInt32Extensions
{
    public static double GetDouble(this uint value)
    {
        return Convert.ToDouble($"{value / 100:0}.{value % 100:00}", CultureInfo.InvariantCulture);
    }

    public static decimal GetDecimal(this uint value)
    {
        return Convert.ToDecimal($"{value / 100:0}.{value % 100:00}", CultureInfo.InvariantCulture);
    }

    public static string GetCurrency(this uint value)
    {
        return $"R$ {value / 100:0}.{value % 100:00}";
    }
}
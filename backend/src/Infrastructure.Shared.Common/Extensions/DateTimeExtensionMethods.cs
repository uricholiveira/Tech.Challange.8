namespace Infrastructure.Shared.Common.Extensions;

public static class DateTimeExtensionMethods
{
    public static bool IsNullOrMinValue(this DateTime? dateTime)
    {
        return dateTime == null || dateTime == DateTime.MinValue;
    }

    public static bool IsSetToMinValue(this DateTime dateTime)
    {
        return dateTime == DateTime.MinValue;
    }
}
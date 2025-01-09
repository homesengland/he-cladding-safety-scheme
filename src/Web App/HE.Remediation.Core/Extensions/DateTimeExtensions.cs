namespace HE.Remediation.Core.Extensions;

public static class DateTimeExtensions
{
    public static DateTime FirstOfMonth(this DateTime date)
    {
        return date.AddDays(-date.Day + 1);
    }

    public static DateTime? FirstOfMonth(this DateTime? date)
    {
        return date?.FirstOfMonth();
    }
}
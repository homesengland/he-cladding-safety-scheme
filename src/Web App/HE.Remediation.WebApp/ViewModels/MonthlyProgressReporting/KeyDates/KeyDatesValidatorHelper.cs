namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public static class KeyDatesValidatorHelper
{
    private readonly static DateTime today = new(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);

    public static bool IsRange(string m, int start, int end)
    {
        if (m == null) return true;
        return int.TryParse(m.ToString(), out var val) && val >= start && val <= end;
    }

    public static bool IsNullOrNumber(string m)
    {
        return m == null || int.TryParse(m.ToString(), out _);
    }

    public static bool IsValidCombination(string m, string y)
    {
        var hasMonth = !string.IsNullOrWhiteSpace(m);
        var hasYear = !string.IsNullOrWhiteSpace(y);
        return (hasMonth && hasYear) || (!hasMonth && !hasYear);
    }

    public static bool BeInFuture(DateTime? date)
    {
        if (!date.HasValue) return true;
        var normalizedDate = new DateTime(date.Value.Year, date.Value.Month, 1);
        return normalizedDate >= today;
    }

    public static bool BeInPast(DateTime? date)
    {
        if (!date.HasValue) return true;
        var normalizedDate = new DateTime(date.Value.Year, date.Value.Month, 1);
        return normalizedDate <= today;
    }
}

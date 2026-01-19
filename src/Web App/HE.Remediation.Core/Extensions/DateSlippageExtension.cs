namespace HE.Remediation.Core.Extensions
{
    public static class DateSlippageExtension
    {
        /// <summary>
        /// Determines if a date has changed by comparing month and year only.
        /// Returns false if previousDate is null (no baseline to compare against).
        /// Returns true if previousDate has a value but newDate is null (date was removed).
        /// Returns true if month or year differs between the dates.
        /// </summary>
        public static bool HasChanged(this DateTime? previousDate, DateTime? newDate)
        {
            if (!previousDate.HasValue)
            {
                return false;
            }

            // Previous date exists but new date is null - date was removed
            // This is considered a change and should trigger the dates changed workflow
            if (!newDate.HasValue)
            {
                return true;
            }

            return previousDate.Value.Month != newDate.Value.Month ||
                    previousDate.Value.Year != newDate.Value.Year;
        }
    }
}

using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
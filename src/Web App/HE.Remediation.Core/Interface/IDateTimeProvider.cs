namespace HE.Remediation.Core.Interface;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
namespace HE.Remediation.Core.Providers.ApplicationDetailsProvider;

public interface IApplicationDetailsProvider
{
    Task<ApplicationDetailsModel> GetApplicationDetails();
}
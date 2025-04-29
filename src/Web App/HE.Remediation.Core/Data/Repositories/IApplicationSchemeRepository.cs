using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.SchemeSelection;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface IApplicationSchemeRepository
    {
        Task<IReadOnlyCollection<ApplicationScheme>> GetApplicationSchemes();
    }
}
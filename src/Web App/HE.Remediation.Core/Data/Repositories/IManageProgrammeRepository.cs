using Dapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface IManageProgrammeRepository
    {
        Task<IReadOnlyCollection<GetApplicationHeadlineResult>> GetApplicationHeadlines(string[] applicationIds);
        Task<IReadOnlyCollection<GetManageProgrammeResponse>> GetManageProgrammeDetails(GetManageProgrammeRequest request, Guid? userId);
        Task SaveManageProgrammeUpdates(SetApplicationUpdatesRequest request);
    }
}

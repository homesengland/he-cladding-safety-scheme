using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface IApplicationRepository
    {
        Task<GetApplicationStatusResult> GetApplicationStatus(Guid applicationId);

        Task UpdateStatusToInProgress(Guid applicationId);
    }
}

using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface IApplicationRepository
    {
        Task<GetApplicationSummaryDetailsResult> GetApplicationSummaryDetails(Guid applicationId);

        Task<GetApplicationStatusResult> GetApplicationStatus(Guid applicationId);

        Task UpdateStatus(Guid applicationId, EApplicationStatus status);

        Task UpdateStatus(Guid applicationId, EApplicationStatus status, string reason);

        Task UpdateApplicationStage(Guid applicationId, EApplicationStage stage);

        Task UpdateInternalStatus(Guid applicationId, EApplicationInternalStatus status); 
        
        Task UpdateInternalStatus(Guid applicationId, EApplicationInternalStatus status, string reason);

        Task<IEnumerable<FileResult>> GetResponsibleEntityEvidenceFiles(Guid applicationId);

        Task<EBankDetailsRelationship> GetBankDetailsRelationship(Guid applicationId);

        Task<string> GetApplicationReferenceNumber(Guid applicationId);

        Task<DateTime?> GetApplicationCreationDate(Guid applicationId);
    }
}

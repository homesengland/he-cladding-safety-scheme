using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories;

public interface IRightToManageRepository
{
    Task CreateRightToManageIfNotExists(Guid applicationId);
    Task<bool?> GetHasAcquiredRightToManage(Guid applicationId);
    Task UpdateHasAcquiredRightToManage(UpdateHasAcquiredRightToManageParameters parameters);
    Task<DateTime?> GetRightToManageAcquisition(Guid applicationId);
    Task UpdateRightToManageAcquisition(UpdateRightToManageAcquisitionParameters parameters);
    Task<IReadOnlyCollection<FileResult>> GetRightToManageEvidence(Guid applicationId);
    Task AddRightToManageEvidence(AddRightToManageEvidenceParameters parameters);
    Task DeleteRightToManageEvidence(DeleteRightToManageEvidenceParameters parameters);
}
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface IFireRiskWorksRepository
    {
        Task<FireRiskWorksRequiredResult> GetWorksRequired(Guid applicationId);
        Task SetExternalWorksRequired(Guid applicationId, ENoYes required);

        Task SetInternalWorksRequired(Guid applicationId, ENoYes required);


        Task<List<GetWallWorksListResult>> GetFireRiskWallWorks(Guid applicationId, EWorkType workType);

        Task<GetWallWorksListResult> GetFireRiskWallWorksDetail(Guid Id);

        Task<List<CladdingSystemsListResult>> GetFireRiskCladdingWorks(Guid applicationId);

        Task InsertWallWorks(EWorkType WorkTypeId, string Description, Guid ApplicationId);

        Task UpdateWallWorks(EWorkType WorkTypeId, string Description, Guid ApplicationId, Guid Id);

        Task DeleteFireRiskWallWorks(Guid Id);

        Task ResetFireRiskWallWorks(Guid applicationId, EWorkType workType);
    }
}

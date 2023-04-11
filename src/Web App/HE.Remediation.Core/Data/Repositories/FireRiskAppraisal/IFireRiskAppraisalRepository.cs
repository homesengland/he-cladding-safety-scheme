using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;

namespace HE.Remediation.Core.Data.Repositories.FireRiskAppraisal
{
    public interface IFireRiskAppraisalRepository
    {
        Task<List<GetFireRiskAssessorListResult>> GetFireAssessorList();
        Task<List<GetCladdingTypeResult>> GetCladdingSystemTypes();
        Task<List<GetInsulationTypeResult>> GetInsulationTypes();
    }
}

using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories
{
    public interface IFireAssessorListRepository
    {
        Task<List<GetFireRiskAssessorListResult>> GetFireAssessorList();
    }
}

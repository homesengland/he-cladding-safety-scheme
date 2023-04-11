using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories
{
    public class FireAssessorListRepository : IFireAssessorListRepository
    {
        private readonly IDbConnectionWrapper _dbConnection;

        public FireAssessorListRepository(IDbConnectionWrapper dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<GetFireRiskAssessorListResult>> GetFireAssessorList()
        {
            var assessors = await _dbConnection.QueryAsync<GetFireRiskAssessorListResult>("GetFireRiskAssessorList");

            return assessors.ToList();
        }
    }
}
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IDbConnectionWrapper _dbConnection;

        public ApplicationRepository(IDbConnectionWrapper dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<GetApplicationStatusResult> GetApplicationStatus(Guid applicationId)
        {
            var applicationStatus = await _dbConnection.QuerySingleOrDefaultAsync<GetApplicationStatusResult>("GetApplicationStatus", new
            {
                ApplicationId = applicationId
            });

            return applicationStatus;
        }

        public async Task UpdateStatusToInProgress(Guid applicationId)
        {
            await _dbConnection.ExecuteAsync("UpdateApplicationStatusToInProgress", new { applicationId });
        }
    }
}
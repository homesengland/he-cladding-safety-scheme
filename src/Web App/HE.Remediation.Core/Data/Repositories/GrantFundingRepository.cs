using Dapper;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories
{
    public class GrantFundingRepository : IGrantFundingRepository
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;

        public GrantFundingRepository(IDbConnectionWrapper dbConnectionWrapper)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
        }
        
        public async Task<DateTime?> GetGrantFundingAgreementCompleteDate(Guid applicationId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ApplicationId", applicationId);

            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<DateTime?>(nameof(GetGrantFundingAgreementCompleteDate), parameters);

            return result;
        }
    }

    public interface IGrantFundingRepository
    {
        /// <summary>
        /// Completion date is when the countersigned document was marked as complete
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns>Date the countersigned document was completed</returns>
        Task<DateTime?> GetGrantFundingAgreementCompleteDate(Guid applicationId);
    }
}

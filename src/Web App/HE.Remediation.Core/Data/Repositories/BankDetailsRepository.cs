using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories
{
    public class BankDetailsRepository : IBankDetailsRepository
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;

        public BankDetailsRepository(IDbConnectionWrapper dbConnectionWrapper)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
        }
        public async Task ResetBankDetails(Guid applicationId)
        {
            await _dbConnectionWrapper.ExecuteAsync("ResetBankDetailsSection", new { applicationId });
        }

        public async Task<GetBankAccountVerificationContactResult> GetBankAccountVerificationContact(Guid applicationId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ApplicationId", applicationId);

            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBankAccountVerificationContactResult>(nameof(GetBankAccountVerificationContact), parameters);

            return result;
        }

        public async Task UpdateBankAccountVerificationContact(UpdateBankAccountVerificationContactParameters parameters)
        {
            await _dbConnectionWrapper.ExecuteAsync(nameof(UpdateBankAccountVerificationContact), parameters);
        }
    }
}

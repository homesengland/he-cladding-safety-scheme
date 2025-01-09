using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class PreTenderRepository : IPreTenderRepository
{
    private readonly IDbConnectionWrapper _db;

    public PreTenderRepository(IDbConnectionWrapper db)
    {
        _db = db;
    }

    public async Task<EBankDetailsRelationship?> GetApplicationBankAccountRelationship(Guid applicationId)
    {
        return await _db.QuerySingleOrDefaultAsync<EBankDetailsRelationship?>("GetApplicationBankAccountRelationship", 
                                                        new 
                                                        { 
                                                            applicationId 
                                                        });
    }

    public async Task<decimal?> GetApplicationPTFSClaimAmount(Guid applicationId)
    {
        return await _db.QuerySingleOrDefaultAsync<decimal?>("GetApplicationPTFSClaimAmount", 
                                                        new 
                                                        { 
                                                            applicationId 
                                                        });
    }

    public async Task<List<GetSignatoryResult>> GetGrantFundingSignatories(Guid applicationId)
    {
        var results = await _db.QueryAsync<GetSignatoryResult>("GetGrantFundingSignatories", new
        {
            ApplicationId = applicationId
        });        
        
        return results.ToList();
    }

    public async Task<bool> IsPreTenderSubmitted(Guid applicationId)
    {
        var result = await _db.QuerySingleOrDefaultAsync<bool>(nameof(IsPreTenderSubmitted), new { ApplicationId = applicationId });
        return result;
    }

    public async Task SubmitPreTender(Guid applicationId)
    {
        await _db.ExecuteAsync(nameof(SubmitPreTender), new { ApplicationId = applicationId });
    }
}

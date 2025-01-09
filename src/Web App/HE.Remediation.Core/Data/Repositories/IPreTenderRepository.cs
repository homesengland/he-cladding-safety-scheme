using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
namespace HE.Remediation.Core.Data.Repositories;

public interface IPreTenderRepository
{
    Task<EBankDetailsRelationship?> GetApplicationBankAccountRelationship(Guid applicationId);

    Task<decimal?> GetApplicationPTFSClaimAmount(Guid applicationId);

    Task<List<GetSignatoryResult>> GetGrantFundingSignatories(Guid applicationId);

    Task<bool> IsPreTenderSubmitted(Guid applicationId);

    Task SubmitPreTender(Guid applicationId);
}

using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories;

public interface IBankDetailsRepository
{
    Task ResetBankDetails(Guid applicationId);

    Task<GetBankAccountVerificationContactResult> GetBankAccountVerificationContact(Guid applicationId);
    Task UpdateBankAccountVerificationContact(UpdateBankAccountVerificationContactParameters parameters);
}
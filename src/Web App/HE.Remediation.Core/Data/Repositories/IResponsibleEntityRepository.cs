using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories;

public interface IResponsibleEntityRepository
{
    Task<CompanyAddressManualDetails> GetCompanyAddress(Guid applicationId);

    Task<FreeholderAddressManualDetails> GetFreeholderAddress(Guid applicationId);

    Task<GetResponsibleEntityCompanyTypeResult> GetResponsibleEntityCompanyType(Guid applicationId);

    Task UpdateFreeholderAddress(Guid applicationId, FreeholderAddressManualDetails addressDetails);

    Task InsertFreeholderAddress(Guid applicationId, FreeholderAddressManualDetails addressDetails);
}
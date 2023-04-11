using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Data.Repositories;

public interface IResponsibleEntityRepository
{
    Task<GetResponsibleEntityCompanyTypeResult> GetResponsibleEntityCompanyType(Guid applicationId);
}
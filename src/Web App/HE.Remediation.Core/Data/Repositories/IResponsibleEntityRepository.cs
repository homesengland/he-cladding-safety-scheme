using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.ResponsibleEntities;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories;

public interface IResponsibleEntityRepository
{
    Task<CompanyAddressManualDetails> GetCompanyAddress(Guid applicationId);

    Task<FreeholderAddressManualDetails> GetFreeholderAddress(Guid applicationId);

    Task<GetResponsibleEntityCompanyTypeResult> GetResponsibleEntityCompanyType(Guid applicationId);

    Task UpdateFreeholderAddress(Guid applicationId, FreeholderAddressManualDetails addressDetails);

    Task InsertFreeholderAddress(Guid applicationId, FreeholderAddressManualDetails addressDetails);

    Task ResetResponsibleEntitiesSection(Guid applicationId);

    Task<GetResponsibleEntityOrganisationAndRepresentationTypeResult> GetResponsibleEntityOrganisationAndRepresentationType(Guid applicationId);

    Task<bool?> GetResponsibleEntityUkRegistered(Guid applicationId);

    Task<int?> GetResponsibleEntityCompanyRelationDetails(Guid applicationId);
    Task SetResponsibleEntityCompanyRelationDetails(SetResponsibleEntityCompanyRelationDetailsParameters parameters);
}

using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanySubType.GetResponsibleEntityCompanySubType;

public class GetResponsibleEntityCompanySubTypeResponse
{
    public EApplicationResponsibleEntityOrganisationSubType? OrganisationSubType { get; set; }
    public string OrganisationSubTypeDescription { get; set; }
}

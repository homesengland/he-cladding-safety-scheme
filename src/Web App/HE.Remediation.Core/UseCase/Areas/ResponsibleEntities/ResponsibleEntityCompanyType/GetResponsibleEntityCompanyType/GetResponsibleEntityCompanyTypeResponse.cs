using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.GetResponsibleEntityCompanyType;

public class GetResponsibleEntityCompanyTypeResponse
{
    public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
    public EApplicationResponsibleEntityOrganisationSubType? OrganisationSubType { get; set; }
    public string OrganisationSubTypeDescription { get; set; }
    public EResponsibleEntityRelation? ResponsibleEntityRelationType { get; set; }
}
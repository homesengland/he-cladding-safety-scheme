using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.GetResponsibleEntityCompanyType;

public class GetResponsibleEntityCompanyTypeResponse
{
    public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
    
    public EResponsibleEntityRelation? ResponsibleEntityRelationType { get; set; }
}
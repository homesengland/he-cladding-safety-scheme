using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.GetResponsibleEntityUkRegistered;

public class GetResponsibleEntityUkRegisteredResponse
{
    public bool? UkRegistered { get; set; }
    public EApplicationResponsibleEntityOrganisationType? CompanyType { get; set; }
}
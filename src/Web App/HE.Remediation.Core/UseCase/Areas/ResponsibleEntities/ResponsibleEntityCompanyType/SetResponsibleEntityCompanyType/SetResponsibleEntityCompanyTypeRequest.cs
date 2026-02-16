using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.SetResponsibleEntityCompanyType;

public class SetResponsibleEntityCompanyTypeRequest : IRequest<Unit>
{
    public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
    
}
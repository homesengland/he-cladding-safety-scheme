using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.SetResponsibleEntityCompanyType;

public class SetResponsibleEntityCompanyTypeRequest : IRequest<Unit>
{
    public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
    
}
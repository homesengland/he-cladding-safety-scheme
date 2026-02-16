
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanySubType.GetResponsibleEntityCompanySubType;

public class GetResponsibleEntityCompanySubTypeRequest : IRequest<GetResponsibleEntityCompanySubTypeResponse>
{
    private GetResponsibleEntityCompanySubTypeRequest()
    {
    }

    public static readonly GetResponsibleEntityCompanySubTypeRequest Request = new();
}

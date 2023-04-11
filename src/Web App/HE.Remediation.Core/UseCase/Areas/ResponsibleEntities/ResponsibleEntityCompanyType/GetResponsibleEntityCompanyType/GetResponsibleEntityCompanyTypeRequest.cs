using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.GetResponsibleEntityCompanyType;

public class GetResponsibleEntityCompanyTypeRequest : IRequest<GetResponsibleEntityCompanyTypeResponse>
{
    private GetResponsibleEntityCompanyTypeRequest()
    {
    }

    public static readonly GetResponsibleEntityCompanyTypeRequest Request = new();
}
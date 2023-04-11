using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.GetResponsibleEntityUkRegistered;

public class GetResponsibleEntityUkRegisteredRequest : IRequest<GetResponsibleEntityUkRegisteredResponse>
{
    private GetResponsibleEntityUkRegisteredRequest()
    {
    }

    public static readonly GetResponsibleEntityUkRegisteredRequest Request = new();
}
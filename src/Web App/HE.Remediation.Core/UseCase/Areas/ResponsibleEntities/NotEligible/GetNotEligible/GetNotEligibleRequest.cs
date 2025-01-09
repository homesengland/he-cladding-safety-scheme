using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.GetNotEligible;

public class GetNotEligibleRequest : IRequest<GetNotEligibleResponse>
{
    private GetNotEligibleRequest()
    {
    }

    public static readonly GetNotEligibleRequest Request = new();
}
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.SetNotEligible;

public class SetNotEligibleRequest : IRequest
{
    private SetNotEligibleRequest()
    {
    }

    public static readonly SetNotEligibleRequest Request = new();
}
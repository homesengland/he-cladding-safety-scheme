using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Set;

public class SetDeclarationRequest : IRequest
{
    public bool? ConfirmedAccuratelyProfiledCosts { get; set; }

    public bool? ConfirmedAwareOfProcess { get; set; }

    public bool? ConfirmedAwareOfVariationApproval { get; set; }
}

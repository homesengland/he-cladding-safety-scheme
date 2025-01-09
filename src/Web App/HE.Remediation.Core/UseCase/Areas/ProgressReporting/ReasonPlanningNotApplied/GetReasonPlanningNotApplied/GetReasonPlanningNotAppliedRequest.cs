using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.GetReasonPlanningNotApplied;

public class GetReasonPlanningNotAppliedRequest : IRequest<GetReasonPlanningNotAppliedResponse>
{
    private GetReasonPlanningNotAppliedRequest()
    {
    }

    public static readonly GetReasonPlanningNotAppliedRequest Request = new();
}

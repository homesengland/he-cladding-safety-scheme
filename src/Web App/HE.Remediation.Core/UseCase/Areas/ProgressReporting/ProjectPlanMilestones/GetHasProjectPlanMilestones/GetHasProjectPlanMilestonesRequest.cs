using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones.GetHasProjectPlanMilestones
{
    public class GetHasProjectPlanMilestonesRequest : IRequest<GetHasProjectPlanMilestonesResponse>
    {
        private GetHasProjectPlanMilestonesRequest()
        {
        }

        public static readonly GetHasProjectPlanMilestonesRequest Request = new();
    }
}


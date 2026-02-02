using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones.SetProjectPlanMilestones
{
    public class SetHasProjectPlanMilestonesRequest : IRequest
    {
        public bool? HasProjectPlanMilestones {  get; set; }
    }
}

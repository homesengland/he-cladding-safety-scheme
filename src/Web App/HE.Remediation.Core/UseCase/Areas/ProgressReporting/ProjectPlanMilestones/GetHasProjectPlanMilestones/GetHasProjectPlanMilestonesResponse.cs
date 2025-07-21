
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones.GetHasProjectPlanMilestones
{
    public class GetHasProjectPlanMilestonesResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool? HasProjectPlanMilestones { get; set; }
        public int Version { get; set; }
        public bool HasVisitedCheckYourAnswers { get; set; }
    }
}

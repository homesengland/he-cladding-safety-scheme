using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.CheckYourAnswers.Get
{
    public class GetCheckYourAnswersResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
        public bool PursuingThirdPartyContribution { get; set; }
        public IEnumerable<string> ContributionTypes { get; set; }
        public decimal ContributionAmount { get; set; }
        public string ContributionNotes { get; set; }
    }
}

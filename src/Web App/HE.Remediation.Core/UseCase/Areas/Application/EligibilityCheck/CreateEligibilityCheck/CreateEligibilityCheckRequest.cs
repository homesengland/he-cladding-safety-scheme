namespace HE.Remediation.Core.UseCase.Areas.Application.EligibilityCheck.CreateEligibilityCheck
{
    public class CreateEligibilityCheckRequest
    {
        public bool ApplyingForTheRemediationOfUnsafeCladding { get; set; }

        public bool AwareOfTheBuildingHeight { get; set; }

        public bool EligibleToCreateApplication { get; set; }

        public bool ReadGuidanceAndPoliciesRelated { get; set; }
    }
}

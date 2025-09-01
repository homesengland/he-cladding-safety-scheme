using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.Core.Enums
{
    public enum EClosingReportTask
    {
        [Display(Name = "Fire Risk Assessment")]
        FireRiskAssessment = 1,

        [Display(Name = "Practical Completion Certificate")]
        PracticalCompletionCertificate = 2,

        [Display(Name = "Building Control Evidence")]
        BuildingControlEvidence = 3,

        [Display(Name = "Communication with Leaseholders")]
        CommunicationWithLeaseholders = 4,

        [Display(Name = "Building Insurance Information")]
        BuildingInsuranceInformation = 5,

        [Display(Name = "Evidence of Third Party Contribution")]
        EvidenceOfThirdPartyContribution = 6,

        [Display(Name = "Ratings for Contractors")]
        RatingsForContractors = 7,

        [Display(Name = "Submit Payment Request")]
        SubmitPaymentRequest = 8,

        [Display(Name = "Upload Final Cost Report")]
        UploadFinalCostReport = 9,

        [Display(Name = "Final Payment Declaration & Submission")]
        FinalPaymentDeclaration = 10
    }
}

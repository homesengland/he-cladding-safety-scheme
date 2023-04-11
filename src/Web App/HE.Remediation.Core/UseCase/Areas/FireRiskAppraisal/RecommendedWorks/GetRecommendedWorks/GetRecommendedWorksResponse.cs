using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.GetRecommendedWorks;

public class GetRecommendedWorksResponse
{
    public string BuildingAddress { get; set; }

    public DateTime? FRAEWInstructedDate { get; set; }

    public string BuildingName { get; set; }

    public DateTime? FRAEWCompletedDate { get; set; }    

    public string CompanyUndertakingReport { get; set; }

    public ERiskType LifeSafetyRiskAssessment { get; set; }

    public EReplacementCladding RecommendCladding { get; set; }

    public ENoYes? RecommendBuildingIntetim { get; set; }

    public int? RecommendedTotalAreaCladding { get; set; }

    public string CaveatsLimitations { get; set; }

    public string RemediationSummary { get; set; }

    public string JustifyRecommendation { get; set; }
}

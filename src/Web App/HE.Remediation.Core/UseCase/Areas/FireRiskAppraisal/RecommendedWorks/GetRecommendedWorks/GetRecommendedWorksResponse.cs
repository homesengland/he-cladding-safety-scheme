using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.GetRecommendedWorks;

public class GetRecommendedWorksResponse
{
    public string ApplicationReferenceNumber { get; set; }
    
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
    public IEnumerable<EInterimMeasuresType> RecommendedInterimMeasuresTypes { get; set; } =
        new List<EInterimMeasuresType>();

    public IEnumerable<ERiskSafetyMitigationType> RiskSafetyMitigationTypes { get; set; } =
        new List<ERiskSafetyMitigationType>();

    public string OtherInterimMeasuresText { get; set; }

    public string SafetyRiskOtherText { get; set; }

    public string OtherRiskMitigationOptionsConsidered { get; set; }

    public bool? PartOfDevelopment { get; set; }

    public string Development { get; set; }
}

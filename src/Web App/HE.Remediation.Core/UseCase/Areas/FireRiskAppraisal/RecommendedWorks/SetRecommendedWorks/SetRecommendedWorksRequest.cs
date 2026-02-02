using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.SetRecommendedWorks;

public class SetRecommendedWorksRequest: IRequest<Unit>
{
    public ERiskType LifeSafetyRiskAssessment { get; set; }

    public EReplacementCladding RecommendCladding { get; set; }

    public ENoYes RecommendBuildingIntetim { get; set; }

    public int RecommendedTotalAreaCladding { get; set; }

    public string CaveatsLimitations { get; set; }

    public string RemediationSummary { get; set; }

    public string JustifyRecommendation { get; set; }

    public string OtherInterimMeasuresText { get; set; }

    public string SafetyRiskOtherText { get; set; }

    public string OtherRiskMitigationOptionsConsidered { get; set; }

    public IEnumerable<EInterimMeasuresType> RecommendedInterimMeasuresTypes { get; set; }

    public IEnumerable<ERiskSafetyMitigationType> RiskSafetyMitigationTypes { get; set; }
}

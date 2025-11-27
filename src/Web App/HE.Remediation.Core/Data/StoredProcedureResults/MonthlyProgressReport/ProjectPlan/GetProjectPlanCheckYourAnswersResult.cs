using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectPlan;

public class GetProjectPlanCheckYourAnswersResult
{
    public EIntentToProceedType? IntentToProceedTypeId { get; set; }
    public bool? InternalAdditionalWork { get; set; }
    public decimal? RemainingAmount { get; set; }
    public bool? EnoughFunds { get; set; }
    public string ProjectPlanDocument { get; set; }
    public string PtsUpliftDocument { get; set; }
}
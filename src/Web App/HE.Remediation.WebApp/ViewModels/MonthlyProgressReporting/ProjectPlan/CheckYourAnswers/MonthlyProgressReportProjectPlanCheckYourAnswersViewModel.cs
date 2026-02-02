using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan.CheckYourAnswers;

public class MonthlyProgressReportProjectPlanCheckYourAnswersViewModel
{
    public EApplicationScheme ApplicationScheme { get; set; }
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public EIntentToProceedType? IntentToProceedType { get; set; }
    public decimal? RemainingAmount { get; set; }
    public bool? EnoughFunds { get; set; }
    public bool? InternalAdditionalWork { get; set; }
    public string ProjectPlanDocument { get; set; }
    public string PtsUpliftDocument { get; set; }
}
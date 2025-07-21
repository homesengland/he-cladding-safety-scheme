using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class IntentToProceedViewModel
{
    public EIntentToProceedType? IntentToProceedType { get; set; }
    public int Version { get; set; }
    public bool? OtherMembersAppointed { get; set; }
    public bool HasGco { get; set; }
    public bool HasGcoRoles { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
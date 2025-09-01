using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class FraDateViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public DateTime? FraDate { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}
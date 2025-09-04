using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class FireRiskAppraisalToExternalWallsViewModel : WorkPackageBaseViewModel
{
    public DateTimeOffset FraewSurveyDate { get; set; }

    public decimal? FraewSurveyCost { get; set; }

    public EReplacementCladding? FraewRemediationType { get; set; }

    public ENoYes? RequiresSubcontractors { get; set; }

    public IList<CladdingSystemSummaryViewModel> CladdingSystems { get; set; }
}
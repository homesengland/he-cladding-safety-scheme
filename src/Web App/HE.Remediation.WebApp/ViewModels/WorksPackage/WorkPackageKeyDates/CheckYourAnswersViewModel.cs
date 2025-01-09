using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageKeyDates;

public class CheckYourAnswersViewModel : WorkPackageBaseViewModel
{
    public DateTime? StartDate { get; set; }
    
    public DateTime? UnsafeCladdingRemovalDate { get; set; }
    
    public DateTime? ExpectedDateForCompletion { get; set; }
}

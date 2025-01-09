using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ChangeYourAnswersViewModel
{
    [Required] 
    public bool? Proceed { get; set; }
}

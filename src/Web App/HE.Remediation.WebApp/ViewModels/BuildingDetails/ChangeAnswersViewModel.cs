using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class ChangeAnswersViewModel
    {
        [Required] public bool? Proceed { get; set; }
    }
}

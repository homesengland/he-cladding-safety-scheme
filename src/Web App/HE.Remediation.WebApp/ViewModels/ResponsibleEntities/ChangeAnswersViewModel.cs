using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class ChangeAnswersViewModel
{
    [Required] public bool? Proceed { get; set; }
}
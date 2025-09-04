using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;
using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class CladdingSystemChangeAnswersViewModel : WorkPackageBaseViewModel
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }

    [Required] public bool? Proceed { get; set; }
}

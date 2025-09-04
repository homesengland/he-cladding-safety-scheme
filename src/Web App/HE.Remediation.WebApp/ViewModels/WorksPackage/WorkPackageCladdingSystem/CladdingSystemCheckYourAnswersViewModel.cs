using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class CladdingSystemCheckYourAnswersViewModel : WorkPackageBaseViewModel
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }

    public string ReplacementCladdingSystemTypeName { get; set; }

    public string ReplacementCladdingManufacturerName { get; set; }

    public string ReplacementOtherCladdingSystemType { get; set; }

	public string ReplacementOtherInsulationType { get; set; }

	public string ReplacementOtherCladdingManufacturer { get; set; }

	public string ReplacementOtherInsulationManufacturer { get; set; }

    public string ReplacementInsulationTypeName { get; set; }

    public string ReplacementInsulationManufacturerName { get; set; }
 
    public int? CladdingSystemArea { get; set; }
}

using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CladdingSystemViewModel : WorkPackageBaseViewModel
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }

    public string CladdingSystemTypeName  { get; set; }

    public string CladdingManufacturerName  { get; set; }

    public string InsulationTypeName { get; set; }

    public string InsulationManufacturerName { get; set; }
}

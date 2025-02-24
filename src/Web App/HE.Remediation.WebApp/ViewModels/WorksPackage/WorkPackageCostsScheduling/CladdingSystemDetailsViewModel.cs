using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CladdingSystemDetailsViewModel : WorkPackageBaseViewModel
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }

    public string CladdingSystemTypeName { get; set; }

    public string CladdingManufacturerName { get; set; }

    public string InsulationTypeName { get; set; }

    public string InsulationManufacturerName { get; set; }

    public int? ReplacementCladdingSystemTypeId { get; set; }

    public int? ReplacementInsulationTypeId { get; set; }

    public int? ReplacementCladdingManufacturerId { get; set; }

    public int? ReplacementInsulationManufacturerId { get; set; }

    public string ReplacementOtherCladdingSystemType { get; set; }

    public string ReplacementOtherInsulationType { get; set; }

    public string ReplacementOtherCladdingManufacturer { get; set; }

    public string ReplacementOtherInsulationManufacturer { get; set; }

    public IEnumerable<CladdingTypeViewModel> CladdingTypes { get; set; }

    public IEnumerable<InsulationTypeViewModel> InsulationTypes { get; set; }

    public IEnumerable<CladdingManufacturerViewModel> CladdingManufacturers { get; set; }

    public IEnumerable<CladdingManufacturerViewModel> InsulationManufacturers { get; set; }
}

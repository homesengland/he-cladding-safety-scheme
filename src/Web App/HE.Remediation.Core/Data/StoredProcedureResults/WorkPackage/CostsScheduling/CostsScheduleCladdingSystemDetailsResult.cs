using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;

public class CostsScheduleCladdingSystemDetailsResult
{
    public Guid? FireRiskCladdingSystemsId { get; set; }

    public string CladdingSystemTypeName { get; set; }

    public string CladdingManufacturerName { get; set; }

    public string InsulationTypeName { get; set; }

    public string InsulationManufacturerName { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }

    public int? ReplacementCladdingSystemTypeId { get; set; }

    public int? ReplacementInsulationTypeId { get; set; }

    public int? ReplacementCladdingManufacturerId { get; set; }

    public int? ReplacementInsulationManufacturerId { get; set; }

    public string ReplacementOtherCladdingSystemType { get; set; }

    public string ReplacementOtherInsulationType { get; set; }

    public string ReplacementOtherCladdingManufacturer { get; set; }

    public string ReplacementOtherInsulationManufacturer { get; set; }
}

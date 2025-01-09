using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;

public class CostsScheduleCladdingSystemResult
{
    public Guid? FireRiskCladdingSystemsId { get; set; }

    public Guid? CostsScheduleCladdingSystemId { get; set; }

    public string CladdingSystemTypeName { get; set; }

    public string CladdingManufacturerName { get; set; }

    public string InsulationTypeName { get; set; }

    public string InsulationManufacturerName { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }
}

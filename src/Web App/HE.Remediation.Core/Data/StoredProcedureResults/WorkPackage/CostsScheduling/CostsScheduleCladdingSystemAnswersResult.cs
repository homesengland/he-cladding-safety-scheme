using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;

public class CostsScheduleCladdingSystemAnswersResult
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }

    public string ReplacementCladdingSystemTypeName { get; set; }

    public string ReplacementOtherCladdingSystemType { get; set; }
    
    public string ReplacementCladdingManufacturerName { get; set; }

    public string ReplacementOtherInsulationManufacturer { get; set; }

    public string ReplacementInsulationTypeName { get; set; }

    public string ReplacementOtherInsulationType { get; set; }
    
    public string ReplacementInsulationManufacturerName { get; set; }

    public string ReplacementOtherCladdingManufacturer { get; set; }
}

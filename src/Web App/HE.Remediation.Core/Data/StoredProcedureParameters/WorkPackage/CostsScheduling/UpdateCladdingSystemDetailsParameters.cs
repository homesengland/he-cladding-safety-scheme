using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;

public class UpdateCladdingSystemDetailsParameters
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public int? ReplacementCladdingSystemTypeId { get; set; }

    public int? ReplacementInsulationTypeId { get; set; }

	public int? ReplacementCladdingManufacturerId { get; set; }

    public int? ReplacementInsulationManufacturerId { get; set; }

    public string ReplacementOtherCladdingSystemType { get; set; }

	public string ReplacementOtherInsulationType { get; set; }

	public string ReplacementOtherInsulationManufacturer { get; set; }

	public string ReplacementOtherCladdingManufacturer { get; set; }

    public int? CladdingSystemArea { get; set; }
}
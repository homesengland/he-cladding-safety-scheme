
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystem.Get;

public class GetCladdingSystemResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }

    public string CladdingSystemTypeName { get; set; }

    public string CladdingManufacturerName { get; set; }

    public string InsulationTypeName { get; set; }

    public string InsulationManufacturerName { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }

    public bool IsSubmitted { get; set; }
}

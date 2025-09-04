
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemDetails.Get;

public class GetCladdingSystemDetailsResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }

    public bool IsSubmitted { get; set; }

    public int? CladdingSystemTypeId { get; set; }


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

    public int? CladdingSystemArea { get; set; }

    public IEnumerable<GetCladdingManufacturerResult> CladdingManufacturers { get; set; }

    public IEnumerable<GetCladdingManufacturerResult> InsulationManufacturers { get; set; }

    public IEnumerable<GetCladdingTypeResult> CladdingTypes { get; set; }

    public IEnumerable<GetInsulationTypeResult> InsulationTypes { get; set; }
}


using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystemCheckYourAnswers.Get;

public class GetCladdingSystemCheckYourAnswersResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }

    public EReplacementCladding? IsBeingRemoved { get; set; }

    public string ReplacementCladdingSystemTypeName { get; set; }

    public string ReplacementOtherCladdingSystemType { get; set; }

    public string ReplacementCladdingManufacturerName { get; set; }

    public string ReplacementInsulationTypeName { get; set; }

    public string ReplacementOtherCladdingManufacturer { get; set; }

    public string ReplacementOtherInsulationType { get; set; }

    public string ReplacementInsulationManufacturerName { get; set; }

    public string ReplacementOtherInsulationManufacturer { get; set; }

    public bool IsSubmitted { get; set; }
}

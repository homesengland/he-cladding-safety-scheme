using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.FireRiskAppraisalToExternalWalls.Get;

public class GetFireRiskAppraisalToExternalWallsResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public DateTime? FraewSurveyDate { get; set; }

    public decimal? FraewSurveyCost { get; set; }

    public EReplacementCladding? FraewRemediationType { get; set; }

    public ENoYes? RequiresSubcontractors { get; set; }

    public bool IsSubmitted { get; set; }

    public IReadOnlyCollection<CostsScheduleFireRiskCladdingSystemItemResult> CladdingSystems { get; set; }
}

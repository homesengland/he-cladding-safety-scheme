using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.BuildingControl;

public class GetProgressReportBuildingControlKeyDatesResult
{
    public DateTime? PreviousBuildingControlExpectedApplicationDate { get; set; }
    public DateTime? PreviousBuildingControlActualApplicationDate { get; set; }
    public DateTime? PreviousBuildingControlValidationDate { get; set; }
    public DateTime? PreviousBuildingControlDecisionDate { get; set; }
    public DateTime? BuildingControlExpectedApplicationDate { get; set; }
    public DateTime? BuildingControlActualApplicationDate { get; set; }
    public DateTime? BuildingControlValidationDate { get; set; }
    public DateTime? BuildingControlDecisionDate { get; set; }
    public string Gateway2Reference { get; set; }
    public EBuildingControlDecisionType? BuildingControlDecisionTypeId { get; set; }
}

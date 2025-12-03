using HE.Remediation.Core.Enums;
namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.KeyDates.BuildingControl;
public class GetUploadBuildingControlDocumentsResult
{
    public IList<FileResult> BuildingControlDocuments { get; set; } = new List<FileResult>();
    public EBuildingControlDecisionType? BuildingControlDecision { get; set; }
}

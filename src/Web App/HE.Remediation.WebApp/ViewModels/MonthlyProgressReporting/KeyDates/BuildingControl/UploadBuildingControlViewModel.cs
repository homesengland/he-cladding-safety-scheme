using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates.BuildingControl;

public class UploadBuildingControlViewModel : FileUploadViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public override string DeleteEndpoint => "/MonthlyProgressReporting/BuildingControl/UploadBuildingControl/Delete";

    public override string[] AcceptedFileTypes => new[] { ".pdf", ".docx", ".doc", ".xls", ".xlsx" };
    public override int NumberOfFilesAllowed => 5;
    public EBuildingControlDecisionType? BuildingControlDecision { get; set; }
}
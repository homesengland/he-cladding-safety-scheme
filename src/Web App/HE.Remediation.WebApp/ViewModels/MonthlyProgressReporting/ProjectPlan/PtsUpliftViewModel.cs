using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan;

public class PtsUpliftViewModel : FileUploadViewModel
{
    public override string DeleteEndpoint => "/MonthlyProgressReporting/ProjectPlan/PtsUplift/Delete";
    public override string[] AcceptedFileTypes => new[] { ".xls", ".xlsx" };
    public override int NumberOfFilesAllowed => 1;

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
}
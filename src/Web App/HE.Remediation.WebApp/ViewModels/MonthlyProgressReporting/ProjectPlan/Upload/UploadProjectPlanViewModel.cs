using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan.Upload;

public class UploadProjectPlanViewModel : FileUploadViewModel
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public override string DeleteEndpoint => "/MonthlyProgressReporting/ProjectPlan/UploadProjectPlan/Delete";

    public override string[] AcceptedFileTypes => new[] { ".pdf", ".docx", ".doc", ".xls", ".xlsx" };
    public override int NumberOfFilesAllowed => 1;
    public DateTime? PreviousUploadDate { get; set; }
    public bool? HasEnoughFunds { get; set; }
}
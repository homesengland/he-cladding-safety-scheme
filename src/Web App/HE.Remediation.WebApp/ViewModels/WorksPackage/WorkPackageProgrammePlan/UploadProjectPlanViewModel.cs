using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;

public class UploadProjectPlanViewModel : FileUploadViewModel
{
    public override string DeleteEndpoint => "/WorksPackage/ProgrammePlan/UploadProjectPlan/Delete";
    public override string[] AcceptedFileTypes => new[] { ".pdf", ".docx", ".doc", ".xls", ".xlsx", ".csv" };
    public override int NumberOfFilesAllowed => 5;

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }

    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
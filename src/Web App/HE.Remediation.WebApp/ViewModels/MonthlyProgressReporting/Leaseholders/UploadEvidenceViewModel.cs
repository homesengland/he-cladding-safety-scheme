using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.Leaseholders;

public class UploadEvidenceViewModel : FileUploadViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public override string[] AcceptedFileTypes => [".pdf"];
    public override int NumberOfFilesAllowed => 5;
    public bool IsSubmitted { get; set; }
    public override string DeleteEndpoint => "DeleteEvidence";
}

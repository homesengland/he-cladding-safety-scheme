using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class LeaseholderResidentEvidenceViewModel : FileUploadViewModel
{
    public override string DeleteEndpoint => "/PaymentRequest/LeaseholderResidentEvidence/Delete";
    public override string[] AcceptedFileTypes => new[] { ".pdf",".msg" };
    public override int NumberOfFilesAllowed => 5;

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? LastCommunicationDateMonth { get; set; }
    public int? LastCommunicationDateYear { get; set; }

    public bool? IsSubmitted { get; set; }

    public bool IsExpired { get; set; }

    public string ReturnUrl { get; set; }
}
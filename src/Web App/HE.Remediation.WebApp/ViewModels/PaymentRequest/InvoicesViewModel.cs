using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class InvoicesViewModel : FileUploadViewModel
{
    public override string DeleteEndpoint => "/PaymentRequest/Invoices/Delete";
    public override string[] AcceptedFileTypes => new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };
    public override int NumberOfFilesAllowed => 20;

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool? IsSubmitted { get; set; }

    public bool IsExpired { get; set; }

    public string ReturnUrl { get; set; }
}
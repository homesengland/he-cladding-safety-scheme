using HE.Remediation.Core.Enums;
using SharedVMS = HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class UploadCostReportViewModel : SharedVMS.FileUploadViewModel
{
    public override string DeleteEndpoint => "/PaymentRequest/UploadCostReport/Delete";
    
    public SharedVMS.File AddedFile { get; set; }

    public override string[] AcceptedFileTypes => new[] { ".pdf" };
    public override int NumberOfFilesAllowed => 5;
    public EResponsibleEntityUploadType UploadType { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool? IsSubmitted { get; set; }

    public bool IsExpired { get; set; }

    public string ReturnUrl { get; set; }
}

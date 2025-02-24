using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.ClosingReport.Shared;
using File = HE.Remediation.WebApp.ViewModels.Shared.File;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class UploadViewModel : ClosingReportBaseViewModel
{
    public EClosingReportFileType UploadType { get; set; }
    public List<File> AddedFiles { get; set; }
    
    public IFormFile File { get; set; }
    
    public string[] AcceptedFileTypes => new[] { ".pdf" };

    public int NumberOfFilesAllowed => 5;
}
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;
using File = HE.Remediation.WebApp.ViewModels.Shared.File;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class UploadLeaseholderEngagementViewModel : ScheduleOfWorksBaseViewModel
{
    public IFormFile File { get; set; }

    public List<File> AddedFiles { get; set; }

    public string DeleteEndpoint => "/ScheduleOfWorks/UploadLeaseholderEngagement/Delete";

    public string[] AcceptedFileTypes => new[] { ".pdf" };

    public int NumberOfFilesAllowed => 5;
}
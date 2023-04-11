using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class UploadFireRiskAppraisalReportViewModel : FileUploadViewModel
    {
        public override string DeleteEndpoint => "/FireRiskAppraisal/DeleteReport";

        public override string[] AcceptedFileTypes => new[] { ".pdf" };

        public override int NumberOfFilesAllowed => 1;
    }
}

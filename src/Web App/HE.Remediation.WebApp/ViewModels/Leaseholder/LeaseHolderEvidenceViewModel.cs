using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class LeaseHolderEvidenceViewModel : FileUploadViewModel
    {
        public override string DeleteEndpoint => "/Leaseholder/Evidence/Delete";
        public override string[] AcceptedFileTypes => new[] { ".pdf" };
        public override int NumberOfFilesAllowed => 5;
    }
}

using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class LeaseHolderEvidenceViewModel : FileUploadViewModel
    {
        public override string DeleteEndpoint => "/Application/LeaseholderEvidence/Delete";
        public override string[] AcceptedFileTypes => new[] { ".pdf" };

        public override int NumberOfFilesAllowed => 5;
    }
}

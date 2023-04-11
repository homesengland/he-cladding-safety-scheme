namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class CheckYourAnswersViewModel
    {
        public List<LeaseHolderEvidenceFile> LeaseHolderEvidenceFiles { get; set; }

        public bool ReadOnly { get; set; }
    }

    public class LeaseHolderEvidenceFile
    {
        public string Name { get; set; }
    }
}

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class AssessorListViewModel
    {
        public string ApplicationReferenceNumber { get; set; }
        public IEnumerable<AssessorCompanyViewModel> AssessorList { get; set; }
    }
}

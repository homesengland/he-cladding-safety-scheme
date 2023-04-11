using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BankAccount
{
    public class BankAccountDetailsViewModel
    {
        public string NameOnTheAccount { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public int? AccountNumber { get; set; }
        public int? SortCode { get; set; }
        public EApplicationRepresentationType? RepresentationType { get; set; }
        public ESubmitAction SubmitAction { get; set; }
    }
}

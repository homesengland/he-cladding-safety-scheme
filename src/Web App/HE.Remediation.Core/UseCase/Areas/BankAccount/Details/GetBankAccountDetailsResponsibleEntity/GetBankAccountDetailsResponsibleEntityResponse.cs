using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetBankAccountDetailsResponsibleEntity
{
    public class GetBankAccountDetailsResponsibleEntityResponse
    {
        public string NameOnTheAccount { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public int? AccountNumber { get; set; }
        public int? SortCode { get; set; }
        public EApplicationRepresentationType? RepresentationType { get; set; }
    }
}
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetBankAccountDetailsRepresentative
{
    public class SetBankAccountDetailsRepresentativeRequest : IRequest
    {
        public string NameOnTheAccount { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public int? AccountNumber { get; set; }
        public int? SortCode { get; set; }
    }
}

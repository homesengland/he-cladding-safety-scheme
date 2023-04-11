using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetBankAccountDetailsResponsibleEntity
{
    public class SetBankAccountDetailsResponsibleEntityRequest : IRequest
    {
        public string NameOnTheAccount { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }
    }
}
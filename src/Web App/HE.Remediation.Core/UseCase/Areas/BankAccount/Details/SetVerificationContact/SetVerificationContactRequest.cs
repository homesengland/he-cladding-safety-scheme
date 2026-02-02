using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetVerificationContact;

public class SetVerificationContactRequest : IRequest
{
    public string ContactName { get; set; }
    public string ContactNumber { get; set; }
}
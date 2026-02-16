using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetVerificationContact;

public class GetVerificationContactRequest : IRequest<GetVerificationContactResponse>
{
    private GetVerificationContactRequest()
    {
    }

    public static readonly GetVerificationContactRequest Request = new();
}
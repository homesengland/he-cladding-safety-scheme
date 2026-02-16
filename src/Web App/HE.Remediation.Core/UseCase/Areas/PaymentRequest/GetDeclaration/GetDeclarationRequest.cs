using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetDeclaration;

public class GetDeclarationRequest : IRequest<GetDeclarationResponse>
{
    private GetDeclarationRequest()
    {
    }

    public static readonly GetDeclarationRequest Request = new();
}

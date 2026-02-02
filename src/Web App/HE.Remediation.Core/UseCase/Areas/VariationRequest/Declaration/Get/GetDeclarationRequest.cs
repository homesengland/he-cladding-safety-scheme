using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Declaration.Get;

public class GetDeclarationRequest : IRequest<GetDeclarationResponse>
{
    private GetDeclarationRequest()
    {
    }

    public static GetDeclarationRequest Request => new();
}

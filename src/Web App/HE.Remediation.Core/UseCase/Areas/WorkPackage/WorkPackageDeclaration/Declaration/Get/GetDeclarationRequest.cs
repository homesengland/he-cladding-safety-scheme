using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDeclaration.Declaration.Get;

public class GetDeclarationRequest : IRequest<GetDeclarationResponse>
{
    private GetDeclarationRequest()
    {
    }

    public static GetDeclarationRequest Request => new();
}

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Declaration.SetConfirmDeclaration
{
    public class SetConfirmDeclarationRequest : IRequest<Unit>
    {
        private SetConfirmDeclarationRequest()
        {

        }

        public static readonly SetConfirmDeclarationRequest Request = new();
    }
}

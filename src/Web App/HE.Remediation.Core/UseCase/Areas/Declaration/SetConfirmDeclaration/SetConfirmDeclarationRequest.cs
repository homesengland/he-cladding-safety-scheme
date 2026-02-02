using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Declaration.SetConfirmDeclaration
{
    public class SetConfirmDeclarationRequest : IRequest<SetConfirmDeclarationResponse>
    {
        private SetConfirmDeclarationRequest()
        {

        }

        public static readonly SetConfirmDeclarationRequest Request = new();
    }
}

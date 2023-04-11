using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.Submit.SetSubmit
{
    public class SetSubmitRequest : IRequest<Unit>
    {
        private SetSubmitRequest()
        {

        }

        public static readonly SetSubmitRequest Request = new();
    }
}

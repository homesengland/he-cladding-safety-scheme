using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.Submit.SetSubmit
{
    public class SetSubmitRequest : IRequest
    {
        private SetSubmitRequest()
        {

        }

        public static readonly SetSubmitRequest Request = new();
    }
}

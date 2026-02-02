using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunication
{
    public class GetResponsibleForCommunicationRequest : IRequest<GetResponsibleForCommunicationResponse>
    {
        private GetResponsibleForCommunicationRequest() { }

        public static readonly GetResponsibleForCommunicationRequest Request = new();
    }
}

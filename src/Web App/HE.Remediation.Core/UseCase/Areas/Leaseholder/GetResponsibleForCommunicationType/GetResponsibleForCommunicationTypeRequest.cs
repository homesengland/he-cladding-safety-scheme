using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunicationType
{
    public class GetResponsibleForCommunicationTypeRequest : IRequest<GetResponsibleForCommunicationTypeResponse>
    {
        private GetResponsibleForCommunicationTypeRequest() { }

        public static readonly GetResponsibleForCommunicationTypeRequest Request = new();
    }
}

using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunication;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCommunicationPartyDetails
{
    public class GetCommunicationPartyDetailsRequest : IRequest<GetCommunicationPartyDetailsResponse>
    {
        private GetCommunicationPartyDetailsRequest() { }

        public static readonly GetCommunicationPartyDetailsRequest Request = new();
    }
}

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetCommunicationPartyDetails
{
    public class SetCommunicationPartyDetailsRequest : IRequest<Unit>
    {
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}

using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunication
{
    public class SetResponsibleForCommunicationRequest : IRequest<Unit>
    {
        public ENoYes ResponsibleForCommunication { get; set; }
    }
}

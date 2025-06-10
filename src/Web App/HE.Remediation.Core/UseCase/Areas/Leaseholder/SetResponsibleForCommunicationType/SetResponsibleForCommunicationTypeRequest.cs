using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunicationType
{
    public class SetResponsibleForCommunicationTypeRequest : IRequest<Unit>
    {
        public EResponsibleForCommunicationType ResponsibleForCommunicationTypeId { get; set; }

        public string? RepresentationOtherText { get; set; }
    }
}


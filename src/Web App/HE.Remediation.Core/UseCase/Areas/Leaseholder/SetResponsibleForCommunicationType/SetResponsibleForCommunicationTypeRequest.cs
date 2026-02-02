using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.SetResponsibleForCommunicationType
{
    public class SetResponsibleForCommunicationTypeRequest : IRequest<Unit>
    {
        public EResponsibleForCommunicationType ResponsibleForCommunicationTypeId { get; set; }

        public string RepresentationOtherText { get; set; }
    }
}


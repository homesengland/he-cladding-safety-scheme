using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Leaseholder.GetResponsibleForCommunicationType
{
    public class GetResponsibleForCommunicationTypeResponse
    {
        public EApplicationRepresentationType? ApplicationRepresentationType { get; set; }

        public EResponsibleForCommunicationType? ResponsibleForCommunicationTypeId { get; set; }

        public string RepresentationOtherText { get; set; }
    }
}

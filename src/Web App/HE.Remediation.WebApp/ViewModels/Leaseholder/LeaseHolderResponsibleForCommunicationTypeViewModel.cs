using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Shared;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class LeaseHolderResponsibleForCommunicationTypeViewModel
    {
        public EResponsibleForCommunicationType? ResponsibleForCommunicationTypeId { get; set; }
        public EApplicationRepresentationType? ApplicationRepresentationType { get; set; }
        public string RepresentationOtherText { get; set; }
    }
}

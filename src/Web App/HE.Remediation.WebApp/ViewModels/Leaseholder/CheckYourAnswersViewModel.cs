using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Leaseholder
{
    public class CheckYourAnswersViewModel
    {
        public List<LeaseHolderEvidenceFile> LeaseHolderEvidenceFiles { get; set; }

        public ENoYes? ResponsibleForCommunication { get; set; }

        public EApplicationRepresentationType? ApplicationRepresentationType { get; set; }
        public EResponsibleForCommunicationType? ResponsibleForCommunicationTypeId { get; set; }
        public string RepresentationOtherText { get; set; }


        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }

        public bool ReadOnly { get; set; }
    }

    public class LeaseHolderEvidenceFile
    {
        public string Name { get; set; }
    }
}

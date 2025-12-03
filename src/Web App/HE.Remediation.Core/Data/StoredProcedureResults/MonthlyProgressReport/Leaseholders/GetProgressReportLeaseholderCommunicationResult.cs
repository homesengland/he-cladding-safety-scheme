using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.Leaseholders
{
    public class GetProgressReportLeaseholderCommunicationResult
    {
        public ENoYes? HasContacted { get; set; }

        public DateTime? LastCommunicationDate { get; set; }
        public DateTime? PreviousLastCommunicationDate { get; set; }
    }
}

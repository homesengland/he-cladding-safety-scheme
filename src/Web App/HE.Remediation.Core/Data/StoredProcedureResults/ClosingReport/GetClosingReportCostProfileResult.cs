using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport
{
    public class GetClosingReportCostProfileResult
    {
        public Guid Id { get; set; }
        public EPaymentRequestCostType Type { get; set; }
        public string ItemName { get; set; }
        public decimal Value { get; set; }
        public bool Paid { get; set; }
    }
}

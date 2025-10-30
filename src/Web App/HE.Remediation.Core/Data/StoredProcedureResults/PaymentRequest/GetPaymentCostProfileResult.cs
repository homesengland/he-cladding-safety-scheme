using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest
{
    public class GetPaymentCostProfileResult
    {
        public Guid Id { get; set; }

        public EPaymentRequestCostType Type { get; set; }

        public string ItemName { get; set; }

        public decimal? Value { get; set; }

        public decimal? ConfirmedValue { get; set; }

        public bool? Paid { get; set; }

        public bool? IsApproved { get; set; }

        public int Order { get; set; }
    }
}

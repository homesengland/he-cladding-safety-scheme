using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest;

public class CostsProfileResult
{
    public Guid Id { get; set; }

    public EScheduleOfWorksCostType Type { get; set; }

    public string ItemName { get; set; }

    public decimal? Amount { get; set; }
    
    public EPaymentStatus? PaymentStatus { get; set; }
}

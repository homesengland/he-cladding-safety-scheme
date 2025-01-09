using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.CostProfile.Get;

public class GetCostProfileResultItem
{
    public Guid Id { get; set; }

    public EScheduleOfWorksCostType Type { get; set; }

    public string ItemName { get; set; }

    public decimal? Amount { get; set; }

    public EPaymentStatus? PaymentStatus { get; set; }
}

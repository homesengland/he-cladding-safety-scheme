using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults.ScheduleOfWorks;

public class CostsProfileResult
{
    public Guid Id { get; set; }

    public EScheduleOfWorksCostType Type { get; set; }

    public string ItemName { get; set; }

    public decimal? Value { get; set; }

    public decimal? ConfirmedValue { get; set; }
    public EPaymentStatus Status { get; set; }
}

using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitPayment;

public class GetSubmitPaymentResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public decimal? TotalGrantFunding { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public decimal MonthlyCostsTotal { get; set; }

    public int ProjectDuration { get; set; }

    public decimal? TotalGrantPaidToDate { get; set; }

    public decimal? UnprofiledGrantFunding { get; set; }
    
    public decimal? CurrentMonthTotal { get; set; }

    public decimal? MissedPaymentTotal { get; set; }

    public decimal? FinalMonthTotal { get; set; }

    public IReadOnlyCollection<MonthlyCost> PaidCosts { get; set; }

    public IReadOnlyCollection<MonthlyCost> MissedPayments { get; set; }

    public MonthlyCost CurrentMonth { get; set; }

    public MonthlyCost AdditionalCost { get; set; }

    public MonthlyCost FinalMonthCost { get; set; }

    public IReadOnlyCollection<MonthlyCost> MonthlyCosts { get; set; } = new List<MonthlyCost>();

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }

    public bool IsItALastSchedulePayment { get; set; }
}

public class MonthlyCost
{
    public Guid Id { get; set; }
    public string MonthName { get; set; }
    public decimal? Amount { get; set; }
    public bool? Paid { get; set; }

    public bool? IsApproved { get; set; }

    public EPaymentRequestStatus Status { get; set; }
}
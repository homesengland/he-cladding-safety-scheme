using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubmitPayment;

public class GetSubmitPaymentResponse
{
    public decimal? TotalGrantFunding { get; set; }

    public DateTimeOffset? ProjectStartDate { get; set; }

    public DateTimeOffset? ProjectEndDate { get; set; }

    public decimal? ApprovedGrantFunding { get; set; }

    public decimal? GrantPaidToDate { get; set; }

    public int ProjectDuration { get; set; }

    public decimal? UnprofiledGrantFunding { get; set; }

    public IList<MonthlyCost> PaidCosts { get; set; }

    public decimal? FinalMonthTotal { get; set; }

    public MonthlyCost FinalMonthCost { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool SubcontractorsRequired { get; set; }
    public bool IsSubmitted { get; set; }
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
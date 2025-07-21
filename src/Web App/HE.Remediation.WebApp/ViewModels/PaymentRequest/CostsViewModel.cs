using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitPayment;
using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest
{
    public class CostsViewModel: PaymentRequestBaseViewModel
    {
        public decimal? TotalGrantFunding { get; set; }

        public DateTimeOffset? ProjectStartDate { get; set; }

        public DateTimeOffset? ProjectEndDate { get; set; }

        public decimal? ApprovedGrantFunding { get; set; }

        public bool HasPtfsPayment { get; set; }

        public bool IsPtfsPaymentPaid { get; set; }

        public decimal? PtfsPayment { get; set; }

        public string PtfsPaymentText { get; set; }

        public decimal? GrantPaidToDate { get; set; }

        public decimal? MissedPaymentTotal { get; set; }

        public int ProjectDuration { get; set; }

        public IList<MonthlyCostViewModel> MonthlyCosts { get; set; }

        public IList<MonthlyCostViewModel> PaidCosts { get; set; }

        public IList<MonthlyCost> MissedPayments { get; set; }

        public decimal? MonthlyCostsTotal { get; set; }

        public decimal? FinalMonthTotal { get; set; }

        public decimal? TotalGrantPaidToDate { get; set; }

        public decimal? UnprofiledGrantFunding { get; set; }

        public MonthlyCostViewModel CurrentMonth { get; set; }

        public MonthlyCostViewModel AdditionalCost { get; set; }

        public string FirstMonthAdditionalCostText { get; set; }

        public decimal? CurrentMonthTotal { get; set; }

        public MonthlyCostViewModel FinalMonthCost { get; set; }

        public bool IsItALastSchedulePayment { get; set; }
    }

    public class MonthlyCostViewModel
    {
        public Guid? Id { get; set; }

        public string MonthName { get; set; }

        public string AmountText { get; set; }

        public decimal? Amount => decimal.TryParse(AmountText, out var amount) ? amount : null;

        public bool Paid { get; set; }

        public EPaymentRequestStatus Status { get; set; }
    }
}

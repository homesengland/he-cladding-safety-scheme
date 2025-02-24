namespace HE.Remediation.WebApp.ViewModels.Shared;

public class MonthlyCostViewModel
{
    public Guid? Id { get; set; }

    public DateTimeOffset? MonthDate { get; set; }

    public string AmountText { get; set; }

    public decimal? Amount => decimal.TryParse(AmountText, out var amount) ? amount : null;
}

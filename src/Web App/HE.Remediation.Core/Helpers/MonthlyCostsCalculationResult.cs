namespace HE.Remediation.Core.Helpers;

public class MonthlyCostsCalculationResult
{
    public decimal TotalMonthlyCosts { get; set; }

    public decimal UnprofiledAmount { get; set; }

    public decimal TotalCurrentCost { get; set; }

    public decimal FinalCost { get; set; }
}

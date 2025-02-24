namespace HE.Remediation.Core.Data.StoredProcedureResults.Costs;

public class MonthlyCostResult
{
    public Guid? Id { get; set; }

    public DateTime? MonthDate { get; set; }

    public decimal? Amount { get; set; }
}

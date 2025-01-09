namespace HE.Remediation.Core.Data.StoredProcedureParameters.ScheduleOfWorks;

public class UpdateCostParameters
{
    public Guid? Id { get; set; }

    public DateTime? Date { get; set; }

    public decimal? Amount { get; set; }

    public bool? AmountIsNull { get; set; }
}
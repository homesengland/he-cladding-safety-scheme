namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class GetAlertsParameters
{
    public int? Limit { get; set; }
    public Guid? ApplicationId { get; set; }
    public Guid? UserId { get; set; }
    public int? AlertTypeId { get; set; }
    public bool IncludeExpired { get; set; } = false;
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool? IsAcknowledged { get; set; }
}
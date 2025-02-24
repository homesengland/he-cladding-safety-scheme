using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetAlertsResult
{
    public Guid Id { get; set; }
    public Guid ApplicationId { get; set; }
    public int AlertTypeId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Acknowledged { get; set; }
}
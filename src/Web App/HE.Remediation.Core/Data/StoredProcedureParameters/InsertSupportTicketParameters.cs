namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class InsertSupportTicketParameters
{
    public Guid ApplicationId { get; set; }
    public int SupportTicketTypeId { get; set; }
    public string Description { get; set; }
}
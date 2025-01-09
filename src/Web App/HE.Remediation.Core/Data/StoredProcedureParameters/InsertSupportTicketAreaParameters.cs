namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class InsertSupportTicketAreaParameters
{
    public Guid SupportTicketId { get; set; }
    public int SupportTicketAreaTypeId { get; set; }
    public string Description { get; set; }
}
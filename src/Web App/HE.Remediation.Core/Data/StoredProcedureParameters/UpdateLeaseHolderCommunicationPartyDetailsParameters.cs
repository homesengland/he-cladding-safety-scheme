namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpdateLeaseHolderCommunicationPartyDetailsParameters
{
    public string ContactName { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public string EmailAddress { get; set; }
    public string ContactNumber { get; set; }
    public Guid? ApplicationLeaseHolderEngagementId { get; set; }
}
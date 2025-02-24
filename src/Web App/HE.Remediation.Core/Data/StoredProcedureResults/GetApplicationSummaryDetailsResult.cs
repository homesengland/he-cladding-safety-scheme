namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetApplicationSummaryDetailsResult
{
    public Guid ApplicationId { get; set; }
    public string PrimaryContactFirstName { get; set; }
    public string PrimaryContactSurname { get; set; }
    public string PrimaryContactEmailAddress { get; set; }
    public string ReferenceNumber { get; set; }
    public string BuildingName { get; set; }
}
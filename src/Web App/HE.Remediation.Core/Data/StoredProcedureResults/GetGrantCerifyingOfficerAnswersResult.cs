namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetGrantCerifyingOfficerAnswersResult
{
    public bool? HasGco { get; set; }
    public string TeamMember { get; set; }
    public string Role { get; set; }
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string Signatory { get; set; }
    public string SignatoryEmailAddress { get; set; }
    public DateTime? DateAppointed { get; set; }
}
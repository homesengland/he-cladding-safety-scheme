namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class SetOtherAssessorParameters
{
    public Guid ApplicationId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CompanyName { get; set; }
    public string CompanyNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Telephone { get; set; }
}
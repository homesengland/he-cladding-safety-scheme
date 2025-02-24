namespace HE.Remediation.Core.Data.StoredProcedureResults;

public class GetGcoDetailsResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string Role { get; set; }
    public int RoleId { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public string EmailAddress { get; set; }
    public string PrimaryContactNumber { get; set; }
    public bool? ContractSigned { get; set; }
    public bool? IndemnityInsurance { get; set; }
    public bool? InvolvedInOriginalInstallation { get; set; }
    public int? CertifyingOfficerResponseId { get; set; }
}
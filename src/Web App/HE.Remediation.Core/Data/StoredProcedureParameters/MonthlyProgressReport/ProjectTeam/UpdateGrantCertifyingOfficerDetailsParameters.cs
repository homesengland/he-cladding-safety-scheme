namespace HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;

public class UpdateGrantCertifyingOfficerDetailsParameters
{
    public Guid TeamMemberId { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public string EmailAddress { get; set; }
    public string PrimaryContactNumber { get; set; }
    public bool ContractSigned { get; set; }
    public bool IndemnityInsurance { get; set; }
    public bool InvolvedInOriginalInstallation { get; set; }
    public string IndemnityInsuranceReason { get; set; }
    public string InvolvedRoleReason { get; set; }
}

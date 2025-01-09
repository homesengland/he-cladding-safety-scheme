namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.GrantCertifyingOfficer;

public class GrantCertifyingOfficerDetailsResult
{
    public string Name { get; set; }

    public string RoleName { get; set; }

    public string CompanyName { get; set; }

    public string CompanyRegistrationNumber { get; set; }

    public string EmailAddress { get; set; }

    public string PrimaryContactNumber { get; set; }

    public bool? ContractSigned { get; set; }

    public bool? IndemnityInsurance { get; set; }

    public string IndemnityInsuranceReason { get; set; }

    public bool? InvolvedInOriginalInstallation { get; set; }

    public string InvolvedRoleReason { get; set; }

    public int CertifyingOfficerResponseId { get; set; }
}

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.GrantCertifyingOfficer;

public class GrantCertifyingOfficerAnswersResult
{
    public string Name { get; set; }
    
    public string RoleName { get; set; }

    public string CompanyName { get; set; }

    public string CompanyRegistrationNumber { get; set; }

    public string EmailAddress { get; set; }

    public string PrimaryContactNumber { get; set; }

    public string CompanyNameNumber { get; set; }

    public string CompanyAddressLine1 { get; set; }

    public string CompanyAddressLine2 { get; set; }
    
    public string CompanyCity { get; set; }
    
    public string CompanyCounty { get; set; }
    
    public string CompanyPostcode { get; set; }

    public DateTime? DateAppointed { get; set; }

    public bool? ContractSigned { get; set; }

    public bool? IndemnityInsurance { get; set; }

    public bool? InvolvedInOriginalInstallation { get; set; }
    public string AuthorisedSignatory1 { get; set; }

    public string AuthorisedSignatory1EmailAddress { get; set; }

    public DateTime? CompaniesDateOfAppointment { get; set; }
}

using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class DetailsViewModel : WorkPackageBaseViewModel
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
}

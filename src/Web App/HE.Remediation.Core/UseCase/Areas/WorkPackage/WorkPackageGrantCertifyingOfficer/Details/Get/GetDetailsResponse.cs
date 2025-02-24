using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Details.Get;

public class GetDetailsResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public string Name { get; set; }

    public ETeamRole? RoleId { get; set; }

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

    public bool? IsCorrectPerson { get; set; }

    public bool? UpdateRequested { get; set; }

    public bool IsSubmitted { get; set; }
}

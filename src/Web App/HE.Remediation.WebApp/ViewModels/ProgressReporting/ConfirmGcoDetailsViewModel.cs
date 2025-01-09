using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ConfirmGcoDetailsViewModel
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string ReturnUrl { get; set; }

    public Guid TeamMemberId { get; set; }
    public string CompanyName { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public int RoleId { get; set; }

    public string CompanyRegistrationNumber { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public bool? IsContractSigned { get; set; }
    public bool? HasIndemnityInsurance { get; set; }
    public bool? IsInvolvedInOriginalInstallation { get; set; }
    public ECertifyingOfficerResponse? CertifyingOfficerResponse { get; set; }

    public int Version { get; set; }
    public bool IsGcoComplete { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}
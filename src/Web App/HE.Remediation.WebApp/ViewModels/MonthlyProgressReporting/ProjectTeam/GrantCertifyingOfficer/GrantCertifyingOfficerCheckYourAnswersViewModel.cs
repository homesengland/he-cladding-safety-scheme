using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GrantCertifyingOfficerCheckYourAnswersViewModel
{
    public Guid Id { get; set; }
    public bool HasGco { get; set; }
    public Guid TeamMemberId { get; set; }
    public string TeamMemberName { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public string EmailAddress { get; set; }
    public string PrimaryContactNumber { get; set; }
    public bool IndemnityInsurance { get; set; }
    public string IndemnityInsuranceReason { get; set; }
    public bool InvolvedInOriginalInstallation { get; set; }
    public string InvolvedRoleReason { get; set; }
    public string TeamMemberRole { get; set; }
    public Guid TeamMemberRoleId { get; set; }
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string AuthorisedSignatory { get; set; }
    public string AuthorisedSignatoryEmailAddress { get; set; }
    public DateTime? AuthorisedSignatoryDateAppointed { get; set; }
    public DateTime? ContractStartDate { get; set; }

    public string ApplicationReferenceNumber { get; set; }
    public string ApplicationBuildingName { get; set; }

    public bool? ContractSigned { get; set; }
}
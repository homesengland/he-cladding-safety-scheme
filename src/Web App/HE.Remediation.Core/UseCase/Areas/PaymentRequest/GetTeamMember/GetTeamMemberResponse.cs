using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetTeamMember;

public class GetTeamMemberResponse
{
    public bool? CostsChanged { get; set; }
    public bool? UnsafeCladdingRemoved { get; set; } 
    public Guid? TeamMemberId { get; set; }
    public ETeamRole Role { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string CompanyRegistration { get; set; }
    public string EmailAddress { get; set; }
    public string PrimaryContactNumber { get; set; }
    public string OtherRole { get; set; }
    public bool? ContractSigned { get; set; }
    public bool? IndemnityInsurance { get; set; }
    public bool? InvolvedInOriginalInstallation { get; set; }
    public string IndemnityInsuranceReason { get; set; }
    public string InvolvedRoleReason { get; set; }
    public EConsiderateConstructorSchemeType? ConsiderateConstructorSchemeType { get; set; }
    public bool? HasChasCertification { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }    

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}

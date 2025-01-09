using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class TeamMemberViewModel : PaymentRequestBaseViewModel
{    
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
    public bool? HasChasCertification { get; set; }
    public EConsiderateConstructorSchemeType? ConsiderateConstructorSchemeType { get; set; }
    public ESubmitAction SubmitAction { get; set; }

    public int Version { get; set; }
}

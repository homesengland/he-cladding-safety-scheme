﻿using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.WorksPackage.Shared;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class TeamMemberViewModel : WorkPackageBaseViewModel
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
    public EConsiderateConstructorSchemeType? ConsiderateConstructorSchemeType { get; set; }
    public bool? HasChasCertification { get; set; }
    public string ConsiderateConstructorSchemeReasonNo { get; set; }
    public string ConsiderateConstructorSchemeReasonDontKnow { get; set; }
    public bool IsCopy { get; set; }
}
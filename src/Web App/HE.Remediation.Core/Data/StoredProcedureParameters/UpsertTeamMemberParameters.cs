namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class UpsertTeamMemberParameters
{
    public Guid? TeamMemberId { get; set; }
    public Guid ProgressReportId { get; set; }
    public int TeamRoleId { get; set; }
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
    public int? ConsiderateConstructorSchemeTypeId { get; set; }
    public bool? HasChasCertification { get; set; }
    public string ConsiderateConstructorSchemeReason { get; set; }
}
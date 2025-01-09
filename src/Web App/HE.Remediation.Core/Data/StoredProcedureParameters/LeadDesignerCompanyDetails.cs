
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters;

public class TeamMemberDetails
{
    public Guid? Id { get; set; }

    public Guid? ProgressReportId { get; set; }

    public string CompanyName { get; set; }

    public string CompanyRegistrationNumber { get; set; }

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public string PrimaryContactNumber { get; set; }

    public string OtherRole { get; set; }

    public ENoYes? ContractSigned { get; set; }
    
    public ENoYes? IndemnityInsurance { get; set; }

    public ENoYes? InvolvedInOriginalInstallation { get; set; }        

    public string IndemnityInsuranceReason { get; set; }

    public string InvolvedRoleReason { get; set; }

    public int RoleId { get; set; }
}

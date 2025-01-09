using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeadDesignerCompanyDetails.GetLeadDesignerCompanyDetails;

public class GetLeadDesignerCompanyDetailsResponse
{
    public Guid? Id { get; set; }
    public string CompanyName { get; set; }

    public string CompanyRegistrationNumber { get; set; }

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public string PrimaryContactNumber { get; set; }

    public ENoYes? ContractSigned { get; set; }

    public ENoYes? IndemnityInsurance { get; set; }

    public ENoYes? LeadDesignerInvolvedInOriginalInstallation { get; set; }

    public string IndemnityInsuranceReason { get; set; }

    public string LeadDesignerInvolvedRoleReason { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
}

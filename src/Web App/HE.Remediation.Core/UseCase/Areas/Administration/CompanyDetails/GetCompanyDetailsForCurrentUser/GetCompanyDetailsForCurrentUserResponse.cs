namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.GetCompanyDetailsForCurrentUser;

public class GetCompanyDetailsForCurrentUserResponse
{
    public string CompanyName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public string UserRoleInCompany { get; set; }
}
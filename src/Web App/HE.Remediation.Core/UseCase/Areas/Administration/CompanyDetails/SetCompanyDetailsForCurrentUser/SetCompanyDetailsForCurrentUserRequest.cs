using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.SetCompanyDetailsForCurrentUser;

public class SetCompanyDetailsForCurrentUserRequest : IRequest<Unit>
{
    public string CompanyName { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public string UserRoleInCompany { get; set; }
}
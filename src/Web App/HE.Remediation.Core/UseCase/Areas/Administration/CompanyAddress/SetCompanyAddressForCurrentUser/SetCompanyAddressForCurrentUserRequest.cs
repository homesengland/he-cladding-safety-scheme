using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.SetCompanyAddressForCurrentUser;

public class SetCompanyAddressForCurrentUserRequest : IRequest
{
    public string SelectedAddressId { get; set; }
}
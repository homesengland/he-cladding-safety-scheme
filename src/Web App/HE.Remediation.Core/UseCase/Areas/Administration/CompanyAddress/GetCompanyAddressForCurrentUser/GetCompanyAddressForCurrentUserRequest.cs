using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.GetCompanyAddressForCurrentUser;

public class GetCompanyAddressForCurrentUserRequest : IRequest<GetCompanyAddressForCurrentUserResponse>
{
    public static readonly GetCompanyAddressForCurrentUserRequest Request = new();
}
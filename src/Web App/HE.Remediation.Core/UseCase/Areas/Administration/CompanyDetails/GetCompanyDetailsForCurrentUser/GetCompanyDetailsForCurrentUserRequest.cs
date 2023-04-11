using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.GetCompanyDetailsForCurrentUser;

public class GetCompanyDetailsForCurrentUserRequest : IRequest<GetCompanyDetailsForCurrentUserResponse>
{
    public static readonly GetCompanyDetailsForCurrentUserRequest Request = new();
}
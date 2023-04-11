using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.GetCompanyDetailsForCurrentUser;

public class GetCompanyDetailsForCurrentUserHandler : IRequestHandler<GetCompanyDetailsForCurrentUserRequest, GetCompanyDetailsForCurrentUserResponse>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;

    public GetCompanyDetailsForCurrentUserHandler(IApplicationDataProvider adp, IDbConnectionWrapper db)
    {
        _adp = adp;
        _db = db;
    }

    public async Task<GetCompanyDetailsForCurrentUserResponse> Handle(GetCompanyDetailsForCurrentUserRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot get current user company details because the current user could be determined.");
        }

        var result = await _db.QuerySingleOrDefaultAsync<GetCompanyDetailsForCurrentUserResponse>(
            "GetCompanyDetailsByUserId", new { userId });

        return result ?? new GetCompanyDetailsForCurrentUserResponse();
    }
}
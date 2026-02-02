using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.GetCompanyAddressForCurrentUser;

public class GetCompanyAddressForCurrentUserHandler 
    : IRequestHandler<GetCompanyAddressForCurrentUserRequest, GetCompanyAddressForCurrentUserResponse>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;

    public GetCompanyAddressForCurrentUserHandler(IApplicationDataProvider adp, IDbConnectionWrapper db)
    {
        _adp = adp;
        _db = db;
    }

    public async ValueTask<GetCompanyAddressForCurrentUserResponse> Handle(
        GetCompanyAddressForCurrentUserRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot get current user company address because the current user could be determined.");
        }

        return await _db.QuerySingleOrDefaultAsync<GetCompanyAddressForCurrentUserResponse>("GetCompanyAddressByUserId", new { userId });
    }
}
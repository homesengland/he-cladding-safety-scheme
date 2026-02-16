using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Dashboard.GetProfile;

public class GetProfileHandler : IRequestHandler<GetProfileRequest, GetProfileResponse>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IDbConnectionWrapper _db;

    public GetProfileHandler(IApplicationDataProvider adp, IDbConnectionWrapper db)
    {
        _adp = adp;
        _db = db;
    }

    public async ValueTask<GetProfileResponse> Handle(GetProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = _adp.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot get fetch the correspondant address details due to an unknown User Id.");
        }

        var result = await _db.QuerySingleOrDefaultAsync<GetProfileResponse>(
            "GetUserResponsibleEntityTypeByUserId", new 
            { 
                userId 
            });

        return result ?? new GetProfileResponse();
    }
}

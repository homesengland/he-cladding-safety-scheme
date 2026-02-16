using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.GetUserContactConsent;

public class GetUserContactConsentHandler : IRequestHandler<GetUserContactConsentRequest, GetUserContactConsentResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _db;

    public GetUserContactConsentHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
    {
        _applicationDataProvider = applicationDataProvider;
        _db = db;
    }

    public async ValueTask<GetUserContactConsentResponse> Handle(GetUserContactConsentRequest request, CancellationToken cancellationToken)
    {
        var userId = _applicationDataProvider.GetUserId();
        return await GetUserContactConsentResponse(request);
    }

    private async ValueTask<GetUserContactConsentResponse> GetUserContactConsentResponse(GetUserContactConsentRequest request)
    {
        var userId = _applicationDataProvider.GetUserId();

        var result = await _db.QuerySingleOrDefaultAsync<GetUserContactConsentResponse>("GetUserContactConsentDetails", new { userId });
        return result ?? new GetUserContactConsentResponse();                        
    }
}

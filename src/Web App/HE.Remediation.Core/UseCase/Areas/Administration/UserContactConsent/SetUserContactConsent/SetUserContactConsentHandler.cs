using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.UserContactConsent.SetUserContactConsent;

public class SetUserContactConsentHandler : IRequestHandler<SetUserContactConsentRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _db;
    private readonly IUserService _userService;

    public SetUserContactConsentHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db, IUserService userService)
    {
        _applicationDataProvider = applicationDataProvider;
        _db = db;
        _userService = userService;
    }

    public async Task<Unit> Handle(SetUserContactConsentRequest request, CancellationToken cancellationToken)
    {
        await SetContactConsentDetails(request);
        return Unit.Value;
    }

    private async Task SetContactConsentDetails(SetUserContactConsentRequest request)
    {
        var userId = _applicationDataProvider.GetUserId();
            
        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot set current user consent details because the current user could be determined.");
        }

        await _db.ExecuteAsync("UpdateUserConsentDetails", new 
        { 
            userId, 
            request.UserConsent
        });

        await _userService.SetUserProfileStageCompletionStatus(
            EUserProfileStage.ContactInfoConsent,
            userId.Value,
            true);

        _applicationDataProvider.SetUserProfileStageCompletionStatus(EUserProfileStage.ContactInfoConsent);
    }
}

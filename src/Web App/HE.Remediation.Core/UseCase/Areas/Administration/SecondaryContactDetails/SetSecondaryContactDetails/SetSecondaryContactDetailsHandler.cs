using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.SetSecondaryContactDetails;

public class SetSecondaryContactDetailsHandler : IRequestHandler<SetSecondaryContactDetailsRequest>
{        
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _db;
    private readonly IUserService _userService;

    public SetSecondaryContactDetailsHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db, IUserService userService)
    {
        _applicationDataProvider = applicationDataProvider;
        _db = db;
        _userService = userService;
    }

    public async Task<Unit> Handle(SetSecondaryContactDetailsRequest request, CancellationToken cancellationToken)
    {
        await SetContactDetails(request);
        return Unit.Value;
    }

    private async Task SetContactDetails(SetSecondaryContactDetailsRequest request)
    {
        var userId = _applicationDataProvider.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot set current user secondary contact details because the current user could be determined.");
        }

        await _db.ExecuteAsync("InsertOrUpdateSecondaryContactDetails", new 
        { 
            userId, 
            request.Name,                
            request.ContactNumber,
            request.EmailAddress
        });

        await _userService.SetUserProfileStageCompletionStatus(
            EUserProfileStage.SecondaryContactInformation,
            userId.Value,
            true);

        _applicationDataProvider.SetUserProfileStageCompletionStatus(
            EUserProfileStage.SecondaryContactInformation);
    }
}
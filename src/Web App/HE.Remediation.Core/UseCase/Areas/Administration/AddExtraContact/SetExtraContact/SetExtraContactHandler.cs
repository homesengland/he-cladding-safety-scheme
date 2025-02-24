using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.SetExtraContact;

public class SetExtraContactHandler: IRequestHandler<SetExtraContactRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _db;
    private readonly IUserService _userService;

    public SetExtraContactHandler(IApplicationDataProvider applicationDataProvider, 
                                  IDbConnectionWrapper db, 
                                  IUserService userService)
    {
        _applicationDataProvider = applicationDataProvider;
        _db = db;
        _userService = userService;
    }

    public async Task<Unit> Handle(SetExtraContactRequest request, CancellationToken cancellationToken)
    {
        var userId = _applicationDataProvider.GetUserId();
        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot set extra contact details request because the current user could be determined.");
        }

        await _db.ExecuteAsync("UpdateUserAddSecondaryContactState", new 
        { 
            userId, 
            request.AddContact
        });

        await _userService.SetUserProfileStageCompletionStatus(
            EUserProfileStage.WantSecondaryContact, 
            userId.Value,
            true);

        _applicationDataProvider.SetUserProfileStageCompletionStatus(EUserProfileStage.WantSecondaryContact);

        return Unit.Value;
    }
}

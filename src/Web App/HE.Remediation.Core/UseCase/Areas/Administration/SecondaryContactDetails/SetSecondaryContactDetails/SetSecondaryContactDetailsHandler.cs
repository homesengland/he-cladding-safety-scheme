using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using Mediator;
using System.Transactions;

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

    public async ValueTask<Unit> Handle(SetSecondaryContactDetailsRequest request, CancellationToken cancellationToken)
    {
        await SetContactDetails(request);
        return Unit.Value;
    }

    private async ValueTask SetContactDetails(SetSecondaryContactDetailsRequest request)
    {
        var userId = _applicationDataProvider.GetUserId();

        if (userId is null)
        {
            throw new EntityNotFoundException(
                "Cannot set current user secondary contact details because the current user could be determined.");
        }

        if ((request.Id == null) || (request.Id == Guid.Empty))
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _db.ExecuteAsync("InsertSecondaryContactDetails", new
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
                
                scope.Complete();
            }
        }
        else
        {
            await _db.ExecuteAsync("UpdateSecondaryContactDetails", new 
            { 
                request.Id,
                userId, 
                request.Name,                
                request.ContactNumber,
                request.EmailAddress
            });
        }
    }
}
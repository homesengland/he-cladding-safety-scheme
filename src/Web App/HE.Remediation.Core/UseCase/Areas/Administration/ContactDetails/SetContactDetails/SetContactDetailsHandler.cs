using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Services.UserService.Enum;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.ContactDetails.SetContactDetails
{
    public class SetContactDetailsHandler : IRequestHandler<SetContactDetailsRequest>
    {        
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;
        private readonly IUserService _userService;

        public SetContactDetailsHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db, IUserService userService)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
            _userService = userService;
        }

        public async Task<Unit> Handle(SetContactDetailsRequest request, CancellationToken cancellationToken)
        {
            await SetContactDetails(request);
            return Unit.Value;
        }

        private async Task SetContactDetails(SetContactDetailsRequest request)
        {
            var userId = _applicationDataProvider.GetUserId();
            
            if (userId is null)
            {
                throw new EntityNotFoundException(
                    "Cannot set current user contact details because the current user could be determined.");
            }

            await _db.ExecuteAsync("UpdateUserContactDetails", new 
            { 
                userId, 
                request.FirstName,
                request.LastName,
                request.ContactNumber
            });

            await _userService.SetUserProfileStageCompletionStatus(
                EUserProfileStage.ContactInformation,
                userId.Value,
                true);

            _applicationDataProvider.SetUserProfileStageCompletionStatus(EUserProfileStage.ContactInformation);
        }
    }
}

using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.NewApplication.SetExistingApplication
{
    public class SetExistingApplicationHandler : IRequestHandler<SetExistingApplicationRequest>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public SetExistingApplicationHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async ValueTask<Unit> Handle(SetExistingApplicationRequest request, CancellationToken cancellationToken)
        {
            var cookieUserId = _applicationDataProvider.GetUserId();

            if(!cookieUserId.HasValue)
            {
                throw new EntityNotFoundException("User identity cannot be established.");
            }

            // check that the user either created the application, is a member of an associated organisation, or 3rd party
            var isPermitted = await _db.QuerySingleOrDefaultAsync<bool>("CheckApplicationPermission", new { request.ApplicationId, UserId = cookieUserId });

            if (!isPermitted)
            {
                throw new EntityNotFoundException("Application not found.");
            }

            var userIdEmailAddressAndSchemeId = await _db.QuerySingleOrDefaultAsync<UserIdEmailAddressAndSchemeId>("GetUserIdEmailAddressAndSchemeIdByApplicationId", new { request.ApplicationId });
            var applicationScheme = (EApplicationScheme)userIdEmailAddressAndSchemeId.ApplicationSchemeId;
            _applicationDataProvider.SetApplicationDetails(request.ApplicationId, applicationScheme, userIdEmailAddressAndSchemeId.EmailAddress);
            
            return Unit.Value;
        }
    }
}

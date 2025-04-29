using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

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

        public async Task<Unit> Handle(SetExistingApplicationRequest request, CancellationToken cancellationToken)
        {
            var cookieUserId = _applicationDataProvider.GetUserId();
            var userIdEmailAddressAndSchemeId = await _db.QuerySingleOrDefaultAsync<UserIdEmailAddressAndSchemeId>("GetUserIdEmailAddressAndSchemeIdByApplicationId", new { request.ApplicationId });
            if ((cookieUserId.HasValue || userIdEmailAddressAndSchemeId.UserId.HasValue) && cookieUserId != userIdEmailAddressAndSchemeId.UserId.Value)
            {
                throw new EntityNotFoundException("Application not found.");
            }

            var applicationScheme = (EApplicationScheme)userIdEmailAddressAndSchemeId.ApplicationSchemeId;
            _applicationDataProvider.SetApplicationDetails(request.ApplicationId, applicationScheme, userIdEmailAddressAndSchemeId.EmailAddress);
            
            return Unit.Value;
        }
    }
}

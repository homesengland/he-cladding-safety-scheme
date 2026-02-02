using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.NewApplication.CreateNewApplication
{
    public class CreateNewApplicationHandler : IRequestHandler<CreateNewApplicationRequest>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public CreateNewApplicationHandler(
            IApplicationDataProvider applicationDataProvider, 
            IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async ValueTask<Unit> Handle(CreateNewApplicationRequest request, CancellationToken cancellationToken)
        {
            var userId = _applicationDataProvider.GetUserId();

            var applicationId = await _db.QuerySingleOrDefaultAsync<Guid>("InsertApplication", new
            {
                UserId = userId,
                StatusId = EApplicationStatus.ApplicationInProgress,
                StageId = EApplicationStage.ApplyForGrant,
                SchemeId = (int)request.ApplicationScheme
            });

            var userIdAndEmailAddress = await _db.QuerySingleOrDefaultAsync<UserIdEmailAddressAndSchemeId>("GetUserIdEmailAddressAndSchemeIdByApplicationId", new { applicationId });

            _applicationDataProvider.SetApplicationDetails(applicationId, request.ApplicationScheme, userIdAndEmailAddress.EmailAddress);

            return Unit.Value;
        }
    }
}

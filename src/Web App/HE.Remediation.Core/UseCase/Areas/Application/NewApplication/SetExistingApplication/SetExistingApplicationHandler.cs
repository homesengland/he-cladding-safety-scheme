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
            var userIdAndEmailAddress = await _db.QuerySingleOrDefaultAsync<UserIdAndEmailAddress>("GetUserIdAndEmailAddressByApplicationId", new { request.ApplicationId });
            if ((cookieUserId.HasValue || userIdAndEmailAddress.UserId.HasValue) && cookieUserId != userIdAndEmailAddress.UserId.Value)
            {
                throw new EntityNotFoundException("Application not found.");
            }

            _applicationDataProvider.SetApplicationIdAndEmailAddress(request.ApplicationId, userIdAndEmailAddress.EmailAddress);
            
            return Unit.Value;
        }
    }
}

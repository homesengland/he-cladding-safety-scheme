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
            var userId = await _db.QuerySingleOrDefaultAsync<Guid?>("GetUserIdByApplicationId", new { request.ApplicationId });
            if ((cookieUserId.HasValue || userId.HasValue) && cookieUserId != userId)
            {
                throw new EntityNotFoundException("Application not found.");
            }

            _applicationDataProvider.SetApplicationId(request.ApplicationId);
            
            return Unit.Value;
        }
    }
}

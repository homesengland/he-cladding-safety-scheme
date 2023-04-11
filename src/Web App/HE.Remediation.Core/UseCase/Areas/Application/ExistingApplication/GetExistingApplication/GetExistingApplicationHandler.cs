using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication
{
    public class GetExistingApplicationHandler : IRequestHandler<GetExistingApplicationRequest, List<GetExistingApplicationResponse>>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public GetExistingApplicationHandler(IDbConnectionWrapper db, IApplicationDataProvider applicationDataProvider)
        {
            _db = db;
            _applicationDataProvider = applicationDataProvider; 
        }

        public async Task<List<GetExistingApplicationResponse>> Handle(GetExistingApplicationRequest request, CancellationToken cancellationToken)
        {
            var userId = _applicationDataProvider.GetUserId();

            if (userId == null)
            {
                throw new InvalidOperationException(
                    $"Unable to determine user id in {nameof(GetExistingApplicationHandler)}");
            }

            var existingApplications = await _db.QueryAsync<GetExistingApplicationResponse>("GetExistingApplications", new { userId});

            if (existingApplications == null)
            {
                return new List<GetExistingApplicationResponse>();
            }

            return existingApplications.ToList();
        }
    }
}

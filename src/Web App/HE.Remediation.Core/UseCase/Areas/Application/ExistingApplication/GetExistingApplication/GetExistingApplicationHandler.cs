using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication
{
    public class GetExistingApplicationHandler : IRequestHandler<GetExistingApplicationRequest, IReadOnlyCollection<GetExistingApplicationResponse>>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public GetExistingApplicationHandler(IDbConnectionWrapper db, IApplicationDataProvider applicationDataProvider)
        {
            _db = db;
            _applicationDataProvider = applicationDataProvider; 
        }

        public async Task<IReadOnlyCollection<GetExistingApplicationResponse>> Handle(GetExistingApplicationRequest request, CancellationToken cancellationToken)
        {
            var existingApplications = await _db.QueryAsync<GetExistingApplicationResponse>("GetExistingApplications", new
            {
                UserId = _applicationDataProvider.GetUserId(),
                Search = string.IsNullOrWhiteSpace(request.Search) ? null : request.Search
            });
            
            return existingApplications;
        }
    }
}

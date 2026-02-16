using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetWorksToCladdingSystems
{
    public class GetWorksToCladdingSystemsHandler : IRequestHandler<GetWorksToCladdingSystemsRequest, IEnumerable<GetWorksToCladdingSystemsResponse>>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetWorksToCladdingSystemsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<IEnumerable<GetWorksToCladdingSystemsResponse>> Handle(GetWorksToCladdingSystemsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var claddingSystems = await _dbConnectionWrapper
                .QueryAsync<GetWorksToCladdingSystemsResponse>("GetWorksToCladdingSystems", new { applicationId });
            return claddingSystems.ToList();
        }
    }
}

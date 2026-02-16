using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.CladdingArea
{
    internal class GetTotalCladdingAreaHandler : IRequestHandler<GetTotalCladdingAreaRequest, GetTotalCladdingAreaResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetTotalCladdingAreaHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetTotalCladdingAreaResponse> Handle(GetTotalCladdingAreaRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            return await _dbConnectionWrapper
                .QuerySingleOrDefaultAsync<GetTotalCladdingAreaResponse>("GetTotalCladdingAreaForApplicationId", new { applicationId });
        }
    }
}

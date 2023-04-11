using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.GetCompletedAppraisal
{
    public class GetCompletedAppraisalHandler : IRequestHandler<GetCompletedAppraisalRequest, GetCompletedAppraisalResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetCompletedAppraisalHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetCompletedAppraisalResponse> Handle(GetCompletedAppraisalRequest request, CancellationToken cancellationToken)
        {
            return await GetCompletedAppraisalStatus(request);
        }

        private async Task<GetCompletedAppraisalResponse> GetCompletedAppraisalStatus(GetCompletedAppraisalRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetCompletedAppraisalResponse>("GetFireRiskCompletedStatus", new 
            { 
                applicationId 
            });

            return result ?? new GetCompletedAppraisalResponse();
        }
    }
}

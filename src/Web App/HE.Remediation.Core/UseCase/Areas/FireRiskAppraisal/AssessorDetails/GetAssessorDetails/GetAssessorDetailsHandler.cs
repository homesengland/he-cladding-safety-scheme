using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.GetAssessorDetails
{
    public class GetAssessorDetailsHandler : IRequestHandler<GetAssessorDetailsRequest, GetAssessorDetailsResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetAssessorDetailsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetAssessorDetailsResponse> Handle(GetAssessorDetailsRequest request, CancellationToken cancellationToken)
        {
            return await GetAssessorDetails(request);
        }

        private async ValueTask<GetAssessorDetailsResponse> GetAssessorDetails(GetAssessorDetailsRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetAssessorDetailsResponse>("GetFireRiskAssessmentAssessorDetails", new
            {
                applicationId
            });

            return result ?? new GetAssessorDetailsResponse();
        }
    }
}

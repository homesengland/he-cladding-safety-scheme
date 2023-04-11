using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.GetBuildingDetailsAnswers
{
    public class GetBuildingDetailsAnswersHandler : IRequestHandler<GetBuildingDetailsAnswersRequest, GetBuildingDetailsAnswersResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetBuildingDetailsAnswersHandler(
            IDbConnectionWrapper dbConnectionWrapper,
            IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetBuildingDetailsAnswersResponse> Handle(GetBuildingDetailsAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetBuildingDetailsAnswers(applicationId);

            return response;
        }

        private async Task<GetBuildingDetailsAnswersResponse> GetBuildingDetailsAnswers(Guid applicationId)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingDetailsAnswersResponse>("GetBuildingDetailsAnswers", new { applicationId });

            return result ?? new GetBuildingDetailsAnswersResponse();
        }
    }
}

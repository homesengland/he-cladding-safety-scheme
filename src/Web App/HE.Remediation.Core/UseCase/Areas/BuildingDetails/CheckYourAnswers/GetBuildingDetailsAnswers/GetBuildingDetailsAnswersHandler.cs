using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.CheckYourAnswers.GetBuildingDetailsAnswers
{
    public class GetBuildingDetailsAnswersHandler : IRequestHandler<GetBuildingDetailsAnswersRequest, GetBuildingDetailsAnswersResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;

        public GetBuildingDetailsAnswersHandler(
            IDbConnectionWrapper dbConnectionWrapper,
            IApplicationDataProvider applicationDataProvider,
            IApplicationRepository applicationRepository)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
        }

        public async ValueTask<GetBuildingDetailsAnswersResponse> Handle(GetBuildingDetailsAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetBuildingDetailsAnswers(applicationId);

            return response;
        }

        private async ValueTask<GetBuildingDetailsAnswersResponse> GetBuildingDetailsAnswers(Guid applicationId)
        {
            var applicationStatus = await _applicationRepository.GetApplicationStatus(applicationId);

            var answers = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingDetailsAnswersResponse>("GetBuildingDetailsAnswers", new { applicationId });

            answers.ReadOnly = applicationStatus.DeclarationConfirmed;

            return answers ?? new GetBuildingDetailsAnswersResponse();
        }
    }
}

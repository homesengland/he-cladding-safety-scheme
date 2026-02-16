using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.GetNameOfDevelopment
{
    public class GetNameOfDevelopmentHandler : IRequestHandler<GetNameOfDevelopmentRequest, GetNameOfDevelopmentResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetNameOfDevelopmentHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetNameOfDevelopmentResponse> Handle(GetNameOfDevelopmentRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetNameOfDevelopment(applicationId);

            return response;
        }

        private async ValueTask<GetNameOfDevelopmentResponse> GetNameOfDevelopment(Guid applicationId)
        {
            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetNameOfDevelopmentResponse>("GetNameOfDevelopment", new { applicationId });

            return response ?? new GetNameOfDevelopmentResponse();
        }
    }
}

using HE.Remediation.Core.Interface;
using MediatR;

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

        public async Task<GetNameOfDevelopmentResponse> Handle(GetNameOfDevelopmentRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetNameOfDevelopment(applicationId);

            return response;
        }

        private async Task<GetNameOfDevelopmentResponse> GetNameOfDevelopment(Guid applicationId)
        {
            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetNameOfDevelopmentResponse>("GetNameOfDevelopment", new { applicationId });

            return response ?? new GetNameOfDevelopmentResponse();
        }
    }
}

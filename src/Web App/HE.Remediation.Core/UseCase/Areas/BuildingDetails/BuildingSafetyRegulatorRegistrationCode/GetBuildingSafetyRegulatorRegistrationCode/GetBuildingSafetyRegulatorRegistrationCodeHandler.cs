using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode
{
    public class GetBuildingSafetyRegulatorRegistrationCodeHandler : IRequestHandler<GetBuildingSafetyRegulatorRegistrationCodeRequest, GetBuildingSafetyRegulatorRegistrationCodeResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetBuildingSafetyRegulatorRegistrationCodeHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetBuildingSafetyRegulatorRegistrationCodeResponse> Handle(GetBuildingSafetyRegulatorRegistrationCodeRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetBuildingSafetyRegulatorRegistrationCode(applicationId);

            return response;
        }

        private async Task<GetBuildingSafetyRegulatorRegistrationCodeResponse> GetBuildingSafetyRegulatorRegistrationCode(Guid applicationId)
        {
            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetBuildingSafetyRegulatorRegistrationCodeResponse>("GetBuildingSafetyRegulatorRegistrationCode", new { applicationId });

            return response ?? new GetBuildingSafetyRegulatorRegistrationCodeResponse();
        }
    }
}

using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingSafetyRegulatorRegistrationCode.SetBuildingSafetyRegulatorRegistrationCode
{
    public class SetBuildingSafetyRegulatorRegistrationCodeHandler : IRequestHandler<SetBuildingSafetyRegulatorRegistrationCodeRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetBuildingSafetyRegulatorRegistrationCodeHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetBuildingSafetyRegulatorRegistrationCodeRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateBuildingSafetyRegulatorRegistrationCode(request, applicationId);

            return Unit.Value;
        }

        private async Task UpdateBuildingSafetyRegulatorRegistrationCode(SetBuildingSafetyRegulatorRegistrationCodeRequest request, Guid applicationId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateBuildingSafetyRegulatorRegistrationCode",
                new
                {
                    applicationId,
                    request.BuildingSafetyRegulatorRegistrationCode
                }
            );
        }
    }
}

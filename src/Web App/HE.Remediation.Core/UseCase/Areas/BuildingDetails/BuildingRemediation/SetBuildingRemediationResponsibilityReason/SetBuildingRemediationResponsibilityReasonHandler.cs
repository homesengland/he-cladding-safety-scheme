using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.SetBuildingRemediationResponsibilityReason
{
    internal class SetBuildingRemediationResponsibilityReasonHandler : IRequestHandler<SetBuildingRemediationResponsibilityReasonRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetBuildingRemediationResponsibilityReasonHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetBuildingRemediationResponsibilityReasonRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateBuildingPartOfDevelopment(request, applicationId);

            return Unit.Value;
        }

        private async Task UpdateBuildingPartOfDevelopment(SetBuildingRemediationResponsibilityReasonRequest request, Guid applicationId)
        {
            var buildingRemediationResponsibilityTypeId = request.BuildingRemediationResponsibilityType;
            await _dbConnectionWrapper.ExecuteAsync("UpdateBuildingRemediationResponsibilityType",
                new
                {
                    applicationId,
                    buildingRemediationResponsibilityTypeId
                }
            );
        }
    }
}
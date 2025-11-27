using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperInformation;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment;

using MediatR;

using System;


namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.GetBuildingRemediationResponsibilityReason
{
    internal class GetBuildingRemediationResponsibilityReasonHandler : IRequestHandler<GetBuildingRemediationResponsibilityReasonRequest, GetBuildingRemediationResponsibilityResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetBuildingRemediationResponsibilityReasonHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetBuildingRemediationResponsibilityResponse> Handle(GetBuildingRemediationResponsibilityReasonRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetBuildingRemediationResponsibilityReason(applicationId);

            return response;
        }

        private async Task<GetBuildingRemediationResponsibilityResponse> GetBuildingRemediationResponsibilityReason(Guid applicationId)
        {
            var BuildingRemediationResponsibilityTypeId = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<int?>("GetBuildingRemediationResponsibilityType", new { applicationId });
            var response = new GetBuildingRemediationResponsibilityResponse
            {
                BuildingRemediationResponsibilityType = BuildingRemediationResponsibilityTypeId.HasValue
                ? (EBuildingRemediationResponsibilityType?)BuildingRemediationResponsibilityTypeId.Value
                 : null,
                ApplicationScheme = _applicationDataProvider.GetApplicationScheme()
            };

            return response;
        }
    }
}

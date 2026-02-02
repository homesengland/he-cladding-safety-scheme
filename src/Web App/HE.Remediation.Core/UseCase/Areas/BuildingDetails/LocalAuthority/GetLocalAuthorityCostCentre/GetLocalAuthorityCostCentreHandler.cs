using Azure;
using HE.Remediation.Core.Data;
using System;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.GetBuildingPartOfDevelopment;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.LocalAuthority.GetLocalAuthorityCostCentre
{
    public class GetLocalAuthorityCostCentreHandler : IRequestHandler<GetLocalAuthorityCostCentreRequest, GetLocalAuthorityCostCentreResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly ILocalAuthorityCostCentreRepository _costcentreRepository;

        public GetLocalAuthorityCostCentreHandler(
            IDbConnectionWrapper dbConnectionWrapper,
            IApplicationDataProvider applicationDataProvider,
            ILocalAuthorityCostCentreRepository costcentreRepository)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _costcentreRepository = costcentreRepository;
        }

        public async ValueTask<GetLocalAuthorityCostCentreResponse> Handle(GetLocalAuthorityCostCentreRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            var costCentres = await _costcentreRepository.GetCostCentres();
            var response = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetLocalAuthorityCostCentreResponse>("GetLocalAuthorityCostCentreId", new { applicationId });
            response.LocalAuthorityCostCentres = costCentres.ToList();

            return response;
        }
    }
}

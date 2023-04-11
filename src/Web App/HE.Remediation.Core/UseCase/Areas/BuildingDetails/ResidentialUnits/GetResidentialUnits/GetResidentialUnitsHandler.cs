﻿using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.GetResidentialUnits
{
    public class GetResidentialUnitsHandler : IRequestHandler<GetResidentialUnitsRequest, GetResidentialUnitsResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetResidentialUnitsHandler(
            IDbConnectionWrapper dbConnectionWrapper,
            IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetResidentialUnitsResponse> Handle(GetResidentialUnitsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetResidentialUnits(applicationId);

            return response;
        }

        private async Task<GetResidentialUnitsResponse> GetResidentialUnits(Guid applicationId)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetResidentialUnitsResponse>("GetResidentialUnits", new { applicationId });

            return result ?? new GetResidentialUnitsResponse();
        }
    }
}

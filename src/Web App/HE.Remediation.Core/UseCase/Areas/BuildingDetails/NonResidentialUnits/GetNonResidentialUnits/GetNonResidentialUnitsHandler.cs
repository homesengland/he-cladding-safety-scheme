﻿using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.GetNonResidentialUnits
{
    public class GetNonResidentialUnitsHandler : IRequestHandler<GetNonResidentialUnitsRequest, GetNonResidentialUnitsResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetNonResidentialUnitsHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetNonResidentialUnitsResponse> Handle(GetNonResidentialUnitsRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var response = await GetNonResidentialUnits(applicationId);

            return response;
        }

        private async Task<GetNonResidentialUnitsResponse> GetNonResidentialUnits(Guid applicationId)
        {
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetNonResidentialUnitsResponse>("GetNonResidentialUnits", new { applicationId });

            return result ?? new GetNonResidentialUnitsResponse();
        }
    }
}

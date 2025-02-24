﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Get
{
    public class GetAdjustEndDateHandler : IRequestHandler<GetAdjustEndDateRequest, GetAdjustEndDateResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IVariationRequestRepository _variationRequestRepository;

        public GetAdjustEndDateHandler(IApplicationDataProvider applicationDataProvider,
                                       IBuildingDetailsRepository buildingDetailsRepository,
                                       IApplicationRepository applicationRepository,
                                       IVariationRequestRepository variationRequestRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _variationRequestRepository = variationRequestRepository;
        }

        public async Task<GetAdjustEndDateResponse> Handle(GetAdjustEndDateRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var adjustEndDateResult = await _variationRequestRepository.GetVariationAdjustEndDate();

            var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

            return new GetAdjustEndDateResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                NewEndMonth = adjustEndDateResult?.NewEndMonth,
                NewEndYear = adjustEndDateResult?.NewEndYear,
                IsSubmitted = isSubmitted
            };
        }
    }
}
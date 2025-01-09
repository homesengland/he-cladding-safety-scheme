using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Confirmation.Get
{
    public class GetConfirmationHandler : IRequestHandler<GetConfirmationRequest, GetConfirmationResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IVariationRequestRepository _variationRequestRepository;

        public GetConfirmationHandler(IApplicationDataProvider applicationDataProvider,
                                      IBuildingDetailsRepository buildingDetailsRepository,
                                      IApplicationRepository applicationRepository,
                                      IVariationRequestRepository variationRequestRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _variationRequestRepository = variationRequestRepository;
        }

        public async Task<GetConfirmationResponse> Handle(GetConfirmationRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var variationRequestId = await _variationRequestRepository.GetLatestVariationRequestId();

            if (!variationRequestId.HasValue)
            {
                throw new EntityNotFoundException(
                     "No valid Variation Request found for this Application.");
            }

            var confirmation = await _variationRequestRepository.GetConfirmation();

            var overview = await _variationRequestRepository.GetOverview(variationRequestId);

            var isSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

            var thirdPartyContributionPursuingTypes =
                await _variationRequestRepository.GetThirdPartyContributionsThirdPartyContributionPursuingTypesDescription(variationRequestId);

            return new GetConfirmationResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                IsCostVariation = confirmation?.IsCostVariation,
                IsScopeVariation = confirmation?.IsScopeVariation,
                IsTimescaleVariation = confirmation?.IsTimescaleVariation,
                IsThirdPartyContributionVariation = confirmation?.IsThirdPartyContributionVariation,
                NewEndMonth = confirmation?.NewEndMonth,
                NewEndYear = confirmation?.NewEndYear,
                ChangeOfScope = confirmation?.ChangeOfScope,
                TotalApprovedFunding = overview?.TotalGrantFunding,
                VariationRequested = confirmation?.VariationRequested,
                TotalRequestedAmount = (overview?.TotalGrantFunding ?? 0) + (confirmation?.VariationRequested ?? 0),
                ThirdPartyContributionTypes = thirdPartyContributionPursuingTypes,
                ThirdPartyContributionAmount = confirmation?.ContributionAmount ?? 0,
                ThirdPartyContributionNotes = confirmation?.ContributionNotes,
                VariationSummary = confirmation?.VariationSummary,
                IsSubmitted = isSubmitted
            };
        }
    }
}

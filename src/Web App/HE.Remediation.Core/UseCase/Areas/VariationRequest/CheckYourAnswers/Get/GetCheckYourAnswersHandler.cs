using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.CheckYourAnswers.Get;

public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
                                      IBuildingDetailsRepository buildingDetailsRepository,
                                      IApplicationRepository applicationRepository,
                                      IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var variationRequestId = await _variationRequestRepository.GetLatestVariationRequestId();

        var checkYourAnswers = await _variationRequestRepository.GetVariationsCheckYourAnswers();

        var overview = await _variationRequestRepository.GetOverview(variationRequestId);

        var newDuration = CalculateProjectDuration(overview?.StartDate, checkYourAnswers?.NewEndYear, checkYourAnswers?.NewEndMonth);

        var thirdPartyContributionPursuingTypes =
            await _variationRequestRepository.GetThirdPartyContributionsThirdPartyContributionPursuingTypesDescription(variationRequestId);

        var usedContractorContingency = await _variationRequestRepository.GetVariationContractorContingency(variationRequestId);

        return new GetCheckYourAnswersResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsCostVariation = checkYourAnswers?.IsCostVariation,
            IsScopeVariation = checkYourAnswers?.IsScopeVariation,
            IsTimescaleVariation = checkYourAnswers?.IsTimescaleVariation,
            IsThirdPartyContributionVariation = checkYourAnswers?.IsThirdPartyContributionVariation,
            NewEndMonth = checkYourAnswers?.NewEndMonth,
            NewEndYear = checkYourAnswers?.NewEndYear,
            Duration = newDuration,
            ChangeOfScope = checkYourAnswers?.ChangeOfScope,
            TotalApprovedFunding = overview?.TotalGrantFunding,
            VariationRequested = checkYourAnswers?.VariationRequested,
            TotalRequestedAmount = (overview?.TotalGrantFunding ?? 0) + (checkYourAnswers?.VariationRequested ?? 0),
            ThirdPartyContributionTypes = thirdPartyContributionPursuingTypes,
            ThirdPartyContributionAmount = checkYourAnswers?.ContributionAmount ?? 0,
            ThirdPartyContributionNotes = checkYourAnswers?.ContributionNotes,
            VariationSummary = checkYourAnswers?.VariationSummary,
            IsSubmitted = checkYourAnswers?.IsSubmitted ?? false,
            UsedContractorContingency = usedContractorContingency.UsedContractorContingency,
            UsedContractorContingencyAdditionalNotes = usedContractorContingency.UsedContractorContingencyDescription

        };
    }

    private int? CalculateProjectDuration(DateTime? startDate, int? endYear, int? endMonth)
    {
        if (startDate is null || endYear is null || endMonth is null)
        {
            return null;
        }

        var endDate = new DateTime(endYear.Value, endMonth.Value, 1).AddMonths(1).AddDays(-1);

        return (endDate.Year - startDate.Value.Year) * 12
            + endDate.Month - startDate.Value.Month
            + 1;
    }
}

using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ThirdPartyContribution.Get;

public class GetThirdPartyContributionHandler : IRequestHandler<GetThirdPartyContributionRequest, GetThirdPartyContributionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetThirdPartyContributionHandler(IApplicationDataProvider applicationDataProvider,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IApplicationRepository applicationRepository, 
                                   IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetThirdPartyContributionResponse> Handle(GetThirdPartyContributionRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var response = await _workPackageRepository.GetThirdPartyContributionsThirdPartyContribution();
        var pursuingTypes = await _workPackageRepository.GetThirdPartyContributionsThirdPartyContributionPursuingTypes();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetThirdPartyContributionResponse
        {
            ContributionPursuingTypes = pursuingTypes,
            ContributionAmount = response.ContributionAmount,
            ContributionNotes = response.ContributionNotes,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted
        };
    }
}

using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.PreferredContractorLinks.Get;

public class GetPreferredContractorLinksHandler : IRequestHandler<GetPreferredContractorLinksRequest, GetPreferredContractorLinksResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetPreferredContractorLinksHandler(IApplicationDataProvider applicationDataProvider,
                                   IBuildingDetailsRepository buildingDetailsRepository,
                                   IApplicationRepository applicationRepository, 
                                   IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetPreferredContractorLinksResponse> Handle(GetPreferredContractorLinksRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var response = await _workPackageRepository.GetCostsSchedulePreferredContractorLinks();
        
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetPreferredContractorLinksResponse
        {
            PreferredContractorLinks = response.PreferredContractorLinks,
            PreferredContractorLinkAdditionalNotes = response?.PreferredContractorLinkAdditionalNotes ?? "",
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            IsSubmitted = isSubmitted,
            SoughtQuotes = response?.SoughtQuotes
        };
    }
}

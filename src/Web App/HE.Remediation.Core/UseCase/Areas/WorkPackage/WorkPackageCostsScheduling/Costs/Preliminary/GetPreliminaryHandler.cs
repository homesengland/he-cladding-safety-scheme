using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Preliminary;

public class GetPreliminaryHandler : IRequestHandler<GetPreliminaryRequest, GetPreliminaryResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetPreliminaryHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<GetPreliminaryResponse> Handle(GetPreliminaryRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costs = await _workPackageRepository.GetWorkPackageCosts();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetPreliminaryResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            MainContractorPreliminariesAmount = costs.MainContractorPreliminariesAmount,
            MainContractorPreliminariesDescription = costs.MainContractorPreliminariesDescription,
            AccessAmount = costs.AccessAmount,
            AccessDescription = costs.AccessDescription,
            MainContractorOverheadAmount = costs.OverheadsAndProfitAmount,
            MainContractorOverheadDescription = costs.OverheadsAndProfitDescription,
            ContractorContingenciesAmount = costs.ContractorContingenciesAmount,
            ContractorContingenciesDescription = costs.ContractorContingenciesDescription,
            IsSubmitted = isSubmitted
        };
    }
}
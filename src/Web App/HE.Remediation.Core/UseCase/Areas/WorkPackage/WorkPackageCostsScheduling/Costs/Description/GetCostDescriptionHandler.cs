using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Description;

public class GetCostDescriptionHandler : IRequestHandler<GetCostDescriptionRequest, GetCostDescriptionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetCostDescriptionHandler(
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

    public async Task<GetCostDescriptionResponse> Handle(GetCostDescriptionRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costs = await _workPackageRepository.GetWorkPackageCosts();
        
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetCostDescriptionResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            UnsafeCladdingRemovalDescription = costs.RemovalOfCladdingDescription,
            NewCladdingDescription = costs.NewCladdingDescription,
            ExternalWorksDescription = costs.OtherEligibleWorkToExternalWallDescription,
            InternalWorksDescription = costs.InternalMitigationWorksDescription,
            MainContractorPreliminariesDescription = costs.MainContractorPreliminariesDescription,
            AccessDescription = costs.AccessDescription,
            MainContractorOverheadDescription = costs.OverheadsAndProfitDescription,
            ContractorContingenciesDescription = costs.ContractorContingenciesDescription,
            FeasibilityStageDescription = costs.FeasibilityStageDescription,
            PostTenderStageDescription = costs.PostTenderStageDescription,
            PropertyManagerDescription = costs.PropertyManagerDescription,
            IrrecoverableVatDescription = costs.IrrecoverableVatDescription,
            IsSubmitted = isSubmitted
        };
    }
}
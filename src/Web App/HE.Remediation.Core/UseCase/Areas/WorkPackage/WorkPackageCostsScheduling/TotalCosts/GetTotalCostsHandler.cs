using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.TotalCosts;

public class GetTotalCostsHandler : IRequestHandler<GetTotalCostsRequest, GetTotalCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetTotalCostsHandler(
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

    public async Task<GetTotalCostsResponse> Handle(GetTotalCostsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costs = await _workPackageRepository.GetWorkPackageCosts();
        
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetTotalCostsResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            UnsafeCladdingRemovalAmount = costs.RemovalOfCladdingAmount,
            NewCladdingAmount = costs.NewCladdingAmount,
            ExternalWorksAmount = costs.OtherEligibleWorkToExternalWallAmount,
            InternalWorksAmount = costs.InternalMitigationWorksAmount,
            MainContractorPreliminariesAmount = costs.MainContractorPreliminariesAmount,
            AccessAmount = costs.AccessAmount,
            MainContractorOverheadAmount = costs.OverheadsAndProfitAmount,
            ContractorContingenciesAmount = costs.ContractorContingenciesAmount,
            FraewSurveyAmount = costs.FraewSurveyAmount,
            FeasibilityStageAmount = costs.FeasibilityStageAmount,
            PostTenderStageAmount = costs.PostTenderStageAmount,
            PropertyManagerAmount = costs.PropertyManagerAmount,
            IrrecoverableVatAmount = costs.IrrecoverableVatAmount,
            IneligibleAmount = costs.IneligibleAmount,
            IsSubmitted = isSubmitted
        };
    }
}
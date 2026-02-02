using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Other;

public class GetOtherCostsHandler : IRequestHandler<GetOtherCostsRequest, GetOtherCostsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetOtherCostsHandler(
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

    public async ValueTask<GetOtherCostsResponse> Handle(GetOtherCostsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costs = await _workPackageRepository.GetWorkPackageCosts();
        
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetOtherCostsResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            FraewSurveyAmount = costs.FraewSurveyAmount,
            FeasibilityStageAmount = costs.FeasibilityStageAmount,
            FeasibilityStageDescription = costs.FeasibilityStageDescription,
            PostTenderStageAmount = costs.PostTenderStageAmount,
            PostTenderStageDescription = costs.PostTenderStageDescription,
            PropertyManagerAmount = costs.PropertyManagerAmount,
            PropertyManagerDescription = costs.PropertyManagerDescription,
            IrrecoverableVatAmount = costs.IrrecoverableVatAmount,
            IrrecoverableVatDescription = costs.IrrecoverableVatDescription,
            IsSubmitted = isSubmitted
        };
    }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.InstallationOfCladding;

public class GetInstallationOfCladdingHandler : IRequestHandler<GetInstallationOfCladdingRequest, GetInstallationOfCladdingResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetInstallationOfCladdingHandler(
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

    public async Task<GetInstallationOfCladdingResponse> Handle(GetInstallationOfCladdingRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costs = await _workPackageRepository.GetWorkPackageCosts();

        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetInstallationOfCladdingResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            NewCladdingAmount = costs.NewCladdingAmount,
            NewCladdingDescription = costs.NewCladdingDescription,
            ExternalWorksAmount = costs.OtherEligibleWorkToExternalWallAmount,
            ExternalWorksDescription = costs.OtherEligibleWorkToExternalWallDescription,
            InternalWorksAmount = costs.InternalMitigationWorksAmount,
            InternalWorksDescription = costs.InternalMitigationWorksDescription,
            IsSubmitted = isSubmitted
        };
    }
}
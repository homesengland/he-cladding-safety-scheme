using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

public class GetHasProjectPlanHandler : IRequestHandler<GetHasProjectPlanRequest, GetHasProjectPlanResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetHasProjectPlanHandler(
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

    public async ValueTask<GetHasProjectPlanResponse> Handle(GetHasProjectPlanRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();
        var hasProjectPlan = await _workPackageRepository.GetHasProgrammePlan(applicationId);

        return new GetHasProjectPlanResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted,
            HasProjectPlan = hasProjectPlan
        };
    }
}

public class GetHasProjectPlanRequest : IRequest<GetHasProjectPlanResponse>
{
    private GetHasProjectPlanRequest()
    {
    }

    public static readonly GetHasProjectPlanRequest Request = new();
}

public class GetHasProjectPlanResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }

    public bool? HasProjectPlan { get; set; }
}

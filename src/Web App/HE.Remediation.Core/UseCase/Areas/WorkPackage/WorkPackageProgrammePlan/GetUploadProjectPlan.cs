using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

public class GetUploadProjectPlanHandler : IRequestHandler<GetUploadProjectPlanRequest, GetUploadProjectPlanResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _wokPackageRepository;

    public GetUploadProjectPlanHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageRepository wokPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _wokPackageRepository = wokPackageRepository;
    }

    public async ValueTask<GetUploadProjectPlanResponse> Handle(GetUploadProjectPlanRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _wokPackageRepository.IsWorkPackageSubmitted();
        var files = await _wokPackageRepository.GetProgrammePlanDocuments(applicationId);

        return new GetUploadProjectPlanResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted,
            AddedFiles = files.ToList()
        };
    }
}

public class GetUploadProjectPlanRequest : IRequest<GetUploadProjectPlanResponse>
{
    private GetUploadProjectPlanRequest()
    {
    }

    public static readonly GetUploadProjectPlanRequest Request = new();
}

public class GetUploadProjectPlanResponse
{
    public List<FileResult> AddedFiles { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;

public class GetBuildingControlHandler : IRequestHandler<GetBuildingControlRequest, GetBuildingControlResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetBuildingControlHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<GetBuildingControlResponse> Handle(GetBuildingControlRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var buildingControlFiles = await _scheduleOfWorksRepository.GetScheduleOfWorksBuildingControlFiles(applicationId);

        return new GetBuildingControlResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = reference,
            IsSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted(),
            AddedFiles = buildingControlFiles.ToList()
        };
    }
}

public class GetBuildingControlRequest : IRequest<GetBuildingControlResponse>
{
    private GetBuildingControlRequest()
    {
    }

    public static GetBuildingControlRequest Request => new();
}

public class GetBuildingControlResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool IsSubmitted { get; set; }
    public List<FileResult> AddedFiles { get; set; }
}
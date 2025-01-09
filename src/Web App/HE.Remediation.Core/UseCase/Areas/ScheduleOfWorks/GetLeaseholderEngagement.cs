using Auth0.AspNetCore.Authentication;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks;

public class GetLeaseholderEngagementHandler : IRequestHandler<GetLeaseholderEngagementRequest, GetLeaseholderEngagementResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public GetLeaseholderEngagementHandler(
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

    public async Task<GetLeaseholderEngagementResponse> Handle(GetLeaseholderEngagementRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var isSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();
        var files = await _scheduleOfWorksRepository.GetScheduleOfWorksLeaseholderEngagementFiles(applicationId);

        return new GetLeaseholderEngagementResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted,
            AddedFiles = files.ToList()
        };
    }
}

public class GetLeaseholderEngagementRequest : IRequest<GetLeaseholderEngagementResponse>
{
    private GetLeaseholderEngagementRequest()
    {
    }

    public static readonly GetLeaseholderEngagementRequest Request = new();
}

public class GetLeaseholderEngagementResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool IsSubmitted { get; set; }
    public List<FileResult> AddedFiles { get; set; }
}
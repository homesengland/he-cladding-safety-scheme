using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.StartedOnSiteMilestone;

public class GetStartedOnSiteHandler : IRequestHandler<GetStartedOnSiteRequest, GetStartedOnSiteResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IMilestoneRepository _milestoneRepository;

    public GetStartedOnSiteHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IMilestoneRepository milestoneRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _milestoneRepository = milestoneRepository;
    }

    public async Task<GetStartedOnSiteResponse> Handle(GetStartedOnSiteRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var startedOnSite = await _milestoneRepository.GetStartOnSite(applicationId);

        return new GetStartedOnSiteResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            StartedOnSiteDate = startedOnSite?.StartOnSiteDate
        };
    }
}

public class GetStartedOnSiteRequest : IRequest<GetStartedOnSiteResponse>
{
    private GetStartedOnSiteRequest()
    {
    }

    public static readonly GetStartedOnSiteRequest Request = new();
}

public class GetStartedOnSiteResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public DateTime? StartedOnSiteDate { get; set; }
}
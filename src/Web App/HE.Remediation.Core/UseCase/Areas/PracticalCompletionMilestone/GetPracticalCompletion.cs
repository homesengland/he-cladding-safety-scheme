using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PracticalCompletionMilestone;

public class GetPracticalCompletionHandler : IRequestHandler<GetPracticalCompletionRequest, GetPracticalCompletionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IMilestoneRepository _milestoneRepository;

    public GetPracticalCompletionHandler(
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

    public async ValueTask<GetPracticalCompletionResponse> Handle(GetPracticalCompletionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var practicalCompletion = await _milestoneRepository.GetPracticalCompletion(applicationId);

        return new GetPracticalCompletionResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            PracticalCompletionDate = practicalCompletion?.PracticalCompletionDate
        };
    }
}

public class GetPracticalCompletionRequest : IRequest<GetPracticalCompletionResponse>
{
    private GetPracticalCompletionRequest()
    {
    }

    public static readonly GetPracticalCompletionRequest Request = new();
}

public class GetPracticalCompletionResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public DateTime? PracticalCompletionDate { get; set; }
}
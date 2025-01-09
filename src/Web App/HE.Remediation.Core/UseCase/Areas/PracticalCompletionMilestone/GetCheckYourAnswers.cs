using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetCheckYourAnswers;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PracticalCompletionMilestone;

public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IMilestoneRepository _milestoneRepository;

    public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository, IBuildingDetailsRepository buildingDetailsRepository, IMilestoneRepository milestoneRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _milestoneRepository = milestoneRepository;
    }

    public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var practicalCompletion = await _milestoneRepository.GetPracticalCompletion(applicationId);

        return new GetCheckYourAnswersResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            PracticalCompletionDate = practicalCompletion.PracticalCompletionDate,
            IsSubmitted = practicalCompletion.IsPracticalCompletionSubmitted
        };
    }
}

public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
{
    private GetCheckYourAnswersRequest()
    {
    }

    public static readonly GetCheckYourAnswersRequest Request = new();
}

public class GetCheckYourAnswersResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public DateTime? PracticalCompletionDate { get; set; }
    public bool IsSubmitted { get; set; }
}
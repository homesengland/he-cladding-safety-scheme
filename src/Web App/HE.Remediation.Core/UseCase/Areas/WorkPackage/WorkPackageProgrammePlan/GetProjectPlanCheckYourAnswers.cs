using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

public class GetProjectPlanCheckYourAnswersHandler : IRequestHandler<GetProjectPlanCheckYourAnswersRequest, GetProjectPlanCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetProjectPlanCheckYourAnswersHandler(
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

    public async ValueTask<GetProjectPlanCheckYourAnswersResponse> Handle(GetProjectPlanCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        var answers = await _workPackageRepository.GetProgrammePlanCheckYourAnswers(applicationId);

        return new GetProjectPlanCheckYourAnswersResponse
        {
            HasProjectPlan = answers.HasProgrammePlan,
            FileNames = answers.FileNames,
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            IsSubmitted = isSubmitted
        };
    }
}

public class GetProjectPlanCheckYourAnswersRequest : IRequest<GetProjectPlanCheckYourAnswersResponse>
{
    private GetProjectPlanCheckYourAnswersRequest()
    {
    }

    public static readonly GetProjectPlanCheckYourAnswersRequest Request = new();
}

public class GetProjectPlanCheckYourAnswersResponse
{
    public bool? HasProjectPlan { get; set; }
    public IList<string> FileNames { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}
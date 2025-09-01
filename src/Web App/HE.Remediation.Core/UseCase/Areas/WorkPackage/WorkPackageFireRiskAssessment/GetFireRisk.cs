using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class GetFireRiskHandler : IRequestHandler<GetFireRiskRequest, GetFireRiskResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetFireRiskHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<GetFireRiskResponse> Handle(GetFireRiskRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var fireRisk = await _fireRiskAssessmentRepository.GetWorkPacakgeFraFireRiskRating(applicationId);
        var assessor = await _fireRiskAssessmentRepository.GetWorkPackageFraAssessorAndDate(applicationId);

        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetWorkPackageFraVisitedCheckYourAnswers(applicationId);

        return new GetFireRiskResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            FireRiskRating = fireRisk?.FireRiskRatingId,
            HasInternalFireSafetyRisks = fireRisk?.HasInternalFireSafetyRisks,
            VisitedCheckYourAnswers = visitedCheckYourAnswers,
            HasOffPanelAssessor = assessor?.FireRiskAssessorListId is null
        };
    }
}

public class GetFireRiskRequest : IRequest<GetFireRiskResponse>
{
    private GetFireRiskRequest()
    {
    }

    public static readonly GetFireRiskRequest Request = new();
}

public class GetFireRiskResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public EFraRiskRating? FireRiskRating { get; set; }
    public bool? HasInternalFireSafetyRisks { get; set; }

    public bool VisitedCheckYourAnswers { get; set; }
    public bool HasOffPanelAssessor { get; set; }
}
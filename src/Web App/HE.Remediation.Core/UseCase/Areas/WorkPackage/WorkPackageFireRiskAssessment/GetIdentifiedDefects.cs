using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class GetIdentifiedDefectsHandler : IRequestHandler<GetIdentifiedDefectsRequest, GetIdentifiedDefectsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetIdentifiedDefectsHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IFireRiskAssessmentRepository fraRepository, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async ValueTask<GetIdentifiedDefectsResponse> Handle(GetIdentifiedDefectsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var defects = await _fireRiskAssessmentRepository.GetWorkPackageFraInternalDefects(applicationId);
        var otherDefect = await _fireRiskAssessmentRepository.GetWorkPackageFraOtherInternalDefect(applicationId);

        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetWorkPackageFraVisitedCheckYourAnswers(applicationId);

        return new GetIdentifiedDefectsResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            InternalFireSafetyDefects = defects.ToList(),
            OtherInternalDefect = otherDefect,
            VisitedCheckYourAnswers = visitedCheckYourAnswers
        };
    }
}

public class GetIdentifiedDefectsRequest : IRequest<GetIdentifiedDefectsResponse>
{
    private GetIdentifiedDefectsRequest()
    {
    }

    public static readonly GetIdentifiedDefectsRequest Request = new();
}

public class GetIdentifiedDefectsResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public IList<EInternalFireSafetyDefect> InternalFireSafetyDefects { get; set; } = new List<EInternalFireSafetyDefect>();
    public string OtherInternalDefect { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
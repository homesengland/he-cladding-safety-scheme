using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetFireRiskHandler : IRequestHandler<GetFireRiskRequest, GetFireRiskResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetFireRiskHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<GetFireRiskResponse> Handle(GetFireRiskRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var fireRisk = await _fireRiskAssessmentRepository.GetFireRiskRating(applicationId);
        var assessor = await _fireRiskAssessmentRepository.GetAssessorAndFraDate(applicationId);
        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

        return new GetFireRiskResponse
        {
            FireRiskRating = fireRisk?.FireRiskRatingId,
            HasInternalFireSafetyRisks = fireRisk?.HasInternalFireSafetyRisks,
            HasOffPanelAssessor = assessor?.FireRiskAssessorListId is null,
            VisitedCheckYourAnswers = visitedCheckYourAnswers
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
    public EFraRiskRating? FireRiskRating { get; set; }
    public bool? HasInternalFireSafetyRisks { get; set; }
    public bool HasOffPanelAssessor { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetIdentifiedDefectsHandler : IRequestHandler<GetIdentifiedDefectsRequest, GetIdentifiedDefectsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetIdentifiedDefectsHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async ValueTask<GetIdentifiedDefectsResponse> Handle(GetIdentifiedDefectsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var defects = await _fireRiskAssessmentRepository.GetInternalFireSafetyDefects(applicationId);
        var otherDefect = await _fireRiskAssessmentRepository.GetOtherInternalSafetyRisk(applicationId);
        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

        return new GetIdentifiedDefectsResponse
        {
            ApplicationScheme = _applicationDataProvider.GetApplicationScheme(),
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
    public EApplicationScheme ApplicationScheme { get; set; }
    public IList<EInternalFireSafetyDefect> InternalFireSafetyDefects { get; set; } = new List<EInternalFireSafetyDefect>();
    public string OtherInternalDefect { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
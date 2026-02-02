using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetFraDateHandler : IRequestHandler<GetFraDateRequest, GetFraDateResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetFraDateHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async ValueTask<GetFraDateResponse> Handle(GetFraDateRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var date = await _fireRiskAssessmentRepository.GetFraDate(applicationId);
        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

        return new GetFraDateResponse
        {
            FraDate = date,
            VisitedCheckYourAnswers = visitedCheckYourAnswers
        };
    }
}

public class GetFraDateRequest : IRequest<GetFraDateResponse>
{
    private GetFraDateRequest()
    {
    }

    public static readonly GetFraDateRequest Request = new();
}

public class GetFraDateResponse
{
    public DateTime? FraDate { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
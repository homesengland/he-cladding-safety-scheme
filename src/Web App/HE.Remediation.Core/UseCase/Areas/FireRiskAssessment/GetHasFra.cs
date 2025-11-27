using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetHasFraHandler : IRequestHandler<GetHasFraRequest, GetHasFraResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetHasFraHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<GetHasFraResponse> Handle(GetHasFraRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var applicationScheme = _applicationDataProvider.GetApplicationScheme();

        var hasFra = await _fireRiskAssessmentRepository.GetHasFra(applicationId);
        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

        return new GetHasFraResponse
        {
            ApplicationScheme = applicationScheme,
            HasFra = hasFra,
            VisitedCheckYourAnswers = visitedCheckYourAnswers
        };
    }
}

public class GetHasFraRequest : IRequest<GetHasFraResponse>
{
    private GetHasFraRequest()
    {
    }

    public static readonly GetHasFraRequest Request = new();
}

public class GetHasFraResponse
{
    public EApplicationScheme ApplicationScheme { get; set; }
    public bool? HasFra { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetAboutThisSectionHandler : IRequestHandler<GetAboutThisSectionRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetAboutThisSectionHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(GetAboutThisSectionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        await _fireRiskAssessmentRepository.CreateFra(applicationId);

        return Unit.Value;
    }
}

public class GetAboutThisSectionRequest : IRequest
{
    private GetAboutThisSectionRequest()
    {
    }

    public static readonly GetAboutThisSectionRequest Request = new();
}
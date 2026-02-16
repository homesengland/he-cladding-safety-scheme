using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetAboutThisSectionHandler : IRequestHandler<GetAboutThisSectionRequest, GetAboutThisSectionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetAboutThisSectionHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async ValueTask<GetAboutThisSectionResponse> Handle(GetAboutThisSectionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        await _fireRiskAssessmentRepository.CreateFra(applicationId);

        var applicationScheme = _applicationDataProvider.GetApplicationScheme();

        return new GetAboutThisSectionResponse() { ApplicationScheme = applicationScheme };
    }
}

public class GetAboutThisSectionRequest : IRequest<GetAboutThisSectionResponse>
{
    private GetAboutThisSectionRequest()
    {
    }

    public static readonly GetAboutThisSectionRequest Request = new();
}

public class GetAboutThisSectionResponse
{
    public EApplicationScheme ApplicationScheme { get; set; }
}
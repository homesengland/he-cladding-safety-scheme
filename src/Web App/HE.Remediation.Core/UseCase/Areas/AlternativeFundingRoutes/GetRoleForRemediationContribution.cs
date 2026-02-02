using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

public class GetRoleForRemediationContributionHandler : IRequestHandler<GetRoleForRemediationContributionRequest, GetRoleForRemediationContributionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IAlternateFundingRepository _alternateFundingRepository;

    public GetRoleForRemediationContributionHandler(IApplicationDataProvider applicationDataProvider, IAlternateFundingRepository alternateFundingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _alternateFundingRepository = alternateFundingRepository;
    }

    public async ValueTask<GetRoleForRemediationContributionResponse> Handle(GetRoleForRemediationContributionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var results = await _alternateFundingRepository.GetPartyPursuedRoles(applicationId);
        var visitedCheckYourAnswers = await _alternateFundingRepository.GetAlternateFundingVisitedCheckYourAnswers(applicationId);
        return new GetRoleForRemediationContributionResponse
        {
            Roles = results.ToList(),
            VisitedCheckYourAnswers = visitedCheckYourAnswers
        };
    }
}

public class GetRoleForRemediationContributionRequest : IRequest<GetRoleForRemediationContributionResponse>
{
    private GetRoleForRemediationContributionRequest()
    {
    }

    public static readonly GetRoleForRemediationContributionRequest Request = new();
}

public class GetRoleForRemediationContributionResponse
{
    public IList<EPartyPursuedRole> Roles { get; set; } = new List<EPartyPursuedRole>();
    public bool VisitedCheckYourAnswers { get; set; }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

public class GetOtherPartiesHandler : IRequestHandler<GetOtherPartiesRequest, GetOtherPartiesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IAlternateFundingRepository _alternateFundingRepository;

    public GetOtherPartiesHandler(IApplicationDataProvider applicationDataProvider, IAlternateFundingRepository alternateFundingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _alternateFundingRepository = alternateFundingRepository;
    }

    public async Task<GetOtherPartiesResponse> Handle(GetOtherPartiesRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var otherPartyPursuedRole = await _alternateFundingRepository.GetOtherPartyPursuedRole(applicationId);

        var visitedCheckYourAnswers = await _alternateFundingRepository.GetAlternateFundingVisitedCheckYourAnswers(applicationId);

        return new GetOtherPartiesResponse
        {
            OtherPartyPursuedRole = otherPartyPursuedRole,
            VisitedCheckYourAnswers = visitedCheckYourAnswers
        };
    }
}

public class GetOtherPartiesRequest : IRequest<GetOtherPartiesResponse>
{
    private GetOtherPartiesRequest()
    {
    }

    public static readonly GetOtherPartiesRequest Request = new();
}

public class GetOtherPartiesResponse
{
    public string OtherPartyPursuedRole { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
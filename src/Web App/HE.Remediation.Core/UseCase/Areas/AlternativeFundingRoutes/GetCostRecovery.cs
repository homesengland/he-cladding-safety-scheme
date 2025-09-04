using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

public class GetCostRecoveryHandler : IRequestHandler<GetCostRecoveryRequest, GetCostRecoveryResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IAlternateFundingRepository _alternateFundingRepository;

    public GetCostRecoveryHandler(IApplicationDataProvider applicationDataProvider, IAlternateFundingRepository alternateFundingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _alternateFundingRepository = alternateFundingRepository;
    }

    public async Task<GetCostRecoveryResponse> Handle(GetCostRecoveryRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var costRecoveryType = await _alternateFundingRepository.GetCostRecoveryType(applicationId);

        var visitedCheckYourAnswers = await _alternateFundingRepository.GetAlternateFundingVisitedCheckYourAnswers(applicationId);

        return new GetCostRecoveryResponse
        {
            CostRecoveryType = costRecoveryType,
            VisitedCheckYourAnswers = visitedCheckYourAnswers
        };
    }
}

public class GetCostRecoveryRequest : IRequest<GetCostRecoveryResponse>
{
    private GetCostRecoveryRequest()
    {
    }

    public static readonly GetCostRecoveryRequest Request = new();
}

public class GetCostRecoveryResponse
{
    public ECostRecoveryType? CostRecoveryType { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}
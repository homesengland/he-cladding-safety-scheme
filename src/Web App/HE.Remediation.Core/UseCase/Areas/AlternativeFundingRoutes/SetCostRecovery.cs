using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

public class SetCostRecoveryHandler : IRequestHandler<SetCostRecoveryRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IAlternateFundingRepository _alternateFundingRepository;

    public SetCostRecoveryHandler(IApplicationDataProvider applicationDataProvider, IAlternateFundingRepository alternateFundingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _alternateFundingRepository = alternateFundingRepository;
    }

    public async ValueTask<Unit> Handle(SetCostRecoveryRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _alternateFundingRepository.SetCostRecoveryType(new SetCostRecoveryTypeParameters
        {
            ApplicationId = applicationId,
            CostRecoveryTypeId = (int)request.CostRecoveryType!.Value
        });

        return Unit.Value;
    }
}

public class SetCostRecoveryRequest : IRequest
{
    public ECostRecoveryType? CostRecoveryType { get; set; }
}
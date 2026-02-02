using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Evidence.Set;

public class SetEvidenceHandler : IRequestHandler<SetEvidenceRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IVariationRequestRepository _variationRequestRepository;

    public SetEvidenceHandler(IApplicationDataProvider applicationDataProvider,
                              IApplicationRepository applicationRepository,
                              IVariationRequestRepository variationRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _variationRequestRepository = variationRequestRepository;
    }

    public ValueTask<Unit> Handle(SetEvidenceRequest request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(Unit.Value);
    }
}

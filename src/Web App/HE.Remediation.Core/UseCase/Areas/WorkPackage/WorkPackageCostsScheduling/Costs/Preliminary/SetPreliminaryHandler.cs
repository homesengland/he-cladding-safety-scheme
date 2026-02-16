using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Preliminary;

public class SetPreliminaryHandler : IRequestHandler<SetPreliminaryRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetPreliminaryHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetPreliminaryRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdatePreliminaryCosts(new UpdatePreliminaryCostsParameters
        {
            MainContractorPreliminariesAmount = request.MainContractorPreliminariesAmount!.Value,
            MainContractorPreliminariesDescription = request.MainContractorPreliminariesDescription,
            AccessAmount = request.AccessAmount!.Value,
            AccessDescription = request.AccessDescription,
            OverheadsAndProfitAmount = request.MainContractorOverheadAmount!.Value,
            OverheadsAndProfitDescription = request.MainContractorOverheadDescription,
            ContractorContingenciesAmount = request.ContractorContingenciesAmount!.Value,
            ContractorContingenciesDescription = request.ContractorContingenciesDescription
        });

        return Unit.Value;
    }
}
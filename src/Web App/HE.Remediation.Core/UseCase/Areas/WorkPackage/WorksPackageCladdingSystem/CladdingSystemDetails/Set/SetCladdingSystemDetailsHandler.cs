using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Enums;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemDetails.Set;

public class SetCladdingSystemDetailsHandler : IRequestHandler<SetCladdingSystemDetailsRequest, Unit>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetCladdingSystemDetailsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetCladdingSystemDetailsRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await UpdateCladdingSystemDetails(request);

        await UpdateStatuses(request);

        scope.Complete();

        return Unit.Value;
    }

    private async Task UpdateCladdingSystemDetails(SetCladdingSystemDetailsRequest request)
    {
        await _workPackageRepository.UpdateCostsScheduleCladdingSystemDetails(new UpdateCladdingSystemDetailsParameters
        {
            FireRiskCladdingSystemsId = request.FireRiskCladdingSystemsId,
            ReplacementCladdingSystemTypeId = request.ReplacementCladdingSystemTypeId,
            ReplacementInsulationTypeId = request.ReplacementInsulationTypeId,
            ReplacementCladdingManufacturerId = request.ReplacementCladdingManufacturerId,
            ReplacementInsulationManufacturerId = request.ReplacementInsulationManufacturerId,
            ReplacementOtherCladdingSystemType = request.ReplacementOtherCladdingSystemType,
            ReplacementOtherInsulationType = request.ReplacementOtherInsulationType,
            ReplacementOtherInsulationManufacturer = request.ReplacementOtherInsulationManufacturer,
            ReplacementOtherCladdingManufacturer = request.ReplacementOtherCladdingManufacturer,
            CladdingSystemArea = request.CladdingSystemArea,
        });
    }

    private async Task UpdateStatuses(SetCladdingSystemDetailsRequest request)
    {
        await _workPackageRepository.UpdateCostsScheduleCladdingSystemStatus(request.FireRiskCladdingSystemsId, ETaskStatus.InProgress);
    }
}

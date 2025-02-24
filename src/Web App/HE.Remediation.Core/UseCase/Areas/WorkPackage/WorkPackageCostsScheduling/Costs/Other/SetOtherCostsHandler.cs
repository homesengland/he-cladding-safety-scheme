using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Other;

public class SetOtherCostsHandler : IRequestHandler<SetOtherCostsRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetOtherCostsHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetOtherCostsRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateOtherCosts(new UpdateOtherCostsParameters
        {
            FeasibilityStageAmount = request.FeasibilityStageAmount!.Value,
            FeasibilityStageDescription = request.FeasibilityStageDescription,
            FraewSurveyAmount = request.FraewSurveyAmount,
            PostTenderStageAmount = request.PostTenderStageAmount!.Value,
            PostTenderStageDescription = request.PostTenderStageDescription,
            PropertyManagerAmount = request.PropertyManagerAmount!.Value,
            PropertyManagerDescription = request.PropertyManagerDescription,
            IrrecoverableVatAmount = request.IrrecoverableVatAmount!.Value,
            IrrecoverableVatDescription = request.IrrecoverableVatDescription
        });

        return Unit.Value;
    }
}
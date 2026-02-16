using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemCheckYourAnswers.Set;

public class SetCladdingSystemCheckYourAnswersHandler : IRequestHandler<SetCladdingSystemCheckYourAnswersRequest, Unit>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetCladdingSystemCheckYourAnswersHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetCladdingSystemCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateCostsScheduleCladdingSystemStatus(request.FireRiskCladdingSystemsId, ETaskStatus.Completed);

        return Unit.Value;
    }
}

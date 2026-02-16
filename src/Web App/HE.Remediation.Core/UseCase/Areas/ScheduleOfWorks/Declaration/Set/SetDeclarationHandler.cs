using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.ScheduleOfWorks;
using HE.Remediation.Core.Enums;

using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Set;

public class SetDeclarationHandler : IRequestHandler<SetDeclarationRequest>
{
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public SetDeclarationHandler(IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async ValueTask<Unit> Handle(SetDeclarationRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _scheduleOfWorksRepository.UpdateDeclaration(new UpdateDeclarationParameters
        {
            ConfirmedAccuratelyProfiledCosts = request.ConfirmedAccuratelyProfiledCosts,
            ConfirmedAwareOfProcess = request.ConfirmedAwareOfProcess,
            ConfirmedAwareOfVariationApproval = request.ConfirmedAwareOfVariationApproval
        });

        var taskStatusesResult = await _scheduleOfWorksRepository.GetScheduleOfWorksTaskStatuses();

        if (taskStatusesResult?.DeclarationStatusId != ETaskStatus.Completed)
        {
            await _scheduleOfWorksRepository.UpdateScheduleOfWorksDeclarationStatus(ETaskStatus.Completed);
        }

        scope.Complete();

        return Unit.Value;
    }
}
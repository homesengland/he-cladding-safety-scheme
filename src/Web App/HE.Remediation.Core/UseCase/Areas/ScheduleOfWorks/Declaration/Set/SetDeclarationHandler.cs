using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.ScheduleOfWorks;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Declaration.Set;

public class SetDeclarationHandler : IRequestHandler<SetDeclarationRequest>
{
    private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;

    public SetDeclarationHandler(IScheduleOfWorksRepository scheduleOfWorksRepository)
    {
        _scheduleOfWorksRepository = scheduleOfWorksRepository;
    }

    public async Task<Unit> Handle(SetDeclarationRequest request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _scheduleOfWorksRepository.UpdateDeclaration(new UpdateDeclarationParameters
        {
            ConfirmedAccuratelyProfiledCosts = request.ConfirmedAccuratelyProfiledCosts,
            ConfirmedAwareOfProcess = request.ConfirmedAwareOfProcess,
            ConfirmedAwareOfVariationApproval = request.ConfirmedAwareOfVariationApproval
        });

        scope.Complete();

        return Unit.Value;
    }
}
using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.KeyDates;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.KeyDates.Set;

public class SetKeyDatesHandler : IRequestHandler<SetKeyDatesRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetKeyDatesHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetKeyDatesRequest request, CancellationToken cancellationToken)
    {
        var keyDates = await _workPackageRepository.GetKeyDates();

        if (keyDates is null)
        {
            await InsertKeyDates(request);
        }
        else
        {
            await UpdateKeyDates(request);
        }

        return Unit.Value;
    }

    private async Task InsertKeyDates(SetKeyDatesRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.InsertKeyDates(new InsertKeyDatesParameters
        {
            StartDate = GetDate(request.StartDateMonth, request.StartDateYear),
            UnsafeCladdingRemovalDate = GetDate(request.UnsafeCladdingRemovalDateMonth,request.UnsafeCladdingRemovalDateYear),
            ExpectedDateForCompletion = GetDate(request.ExpectedDateForCompletionMonth,request.ExpectedDateForCompletionYear)
        });

        await _workPackageRepository.UpdateKeyDatesStatus(ETaskStatus.InProgress);

        scope.Complete();
    }

    private async Task UpdateKeyDates(SetKeyDatesRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.UpdateKeyDates(new UpdateKeyDatesParameters
        {
            StartDate = GetDate(request.StartDateMonth, request.StartDateYear),
            UnsafeCladdingRemovalDate = GetDate(request.UnsafeCladdingRemovalDateMonth,request.UnsafeCladdingRemovalDateYear),
            ExpectedDateForCompletion = GetDate(request.ExpectedDateForCompletionMonth,request.ExpectedDateForCompletionYear)
        });

        await _workPackageRepository.UpdateKeyDatesStatus(ETaskStatus.InProgress);

        scope.Complete();
    }

    private DateTime? GetDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1).AddMonths(1).AddDays(-1)
            : null;
    }
}
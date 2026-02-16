using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates;

public class SetKeyDatesHandler : IRequestHandler<SetKeyDatesRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetKeyDatesHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<Unit> Handle(SetKeyDatesRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        await _keyDatesRepository.UpdateKeyDatesStatus(new SetKeyDatesStatusParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            TaskStatusId = request.TaskStatusId
        });

        return Unit.Value;
    }
}

public class SetKeyDatesRequest(ETaskStatus taskStatusId) : IRequest
{
    public ETaskStatus TaskStatusId { get; set; } = taskStatusId;
}
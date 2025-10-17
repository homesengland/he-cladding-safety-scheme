using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.ProceedFromAbout;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.UpdateTaskStatus;

public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly IClosingReportRepository _closingReportRepository;

    public UpdateTaskStatusHandler(
        IApplicationDataProvider adp, 
        IClosingReportRepository closingReportRepository)
    {
        _adp = adp;
        _closingReportRepository = closingReportRepository;
    }

    public async Task<Unit> Handle(UpdateTaskStatusRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _adp.GetApplicationId();
        await _closingReportRepository.UpsertClosingReportTaskStatus(applicationId, request.ClosingReportTask, request.TaskStatus, request.AllowRevert);
        return Unit.Value;
    }
}

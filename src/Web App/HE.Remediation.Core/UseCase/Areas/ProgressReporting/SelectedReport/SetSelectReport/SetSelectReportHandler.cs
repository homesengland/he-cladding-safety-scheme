using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SelectedReport.SetSelectReport;

public class SetSelectReportHandler : IRequestHandler<SetSelectReportRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetSelectReportHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public Task<Unit> Handle(SetSelectReportRequest request, CancellationToken cancellationToken)
    {
        _progressReportingRepository.SetProgressReportId(request.Id);

        return Task.FromResult(Unit.Value);
    }
}
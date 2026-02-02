
using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.SetReasonNeedsSupport;

public class SetReasonNeedsSupportHandler : IRequestHandler<SetReasonNeedsSupportRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetReasonNeedsSupportHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetReasonNeedsSupportRequest request, CancellationToken cancellationToken)
    {
        await UpdateLeadDesignerAppointed(request);
        return Unit.Value;
    }

    private async Task UpdateLeadDesignerAppointed(SetReasonNeedsSupportRequest request)
    {
        await _progressReportingRepository.UpdateProgressReportSupportNeededReason(request.SupportNeededReason);
    }
}

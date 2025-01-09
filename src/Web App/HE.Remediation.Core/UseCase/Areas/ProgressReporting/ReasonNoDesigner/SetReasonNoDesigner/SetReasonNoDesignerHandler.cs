
using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoDesigner.SetReasonNoDesigner;

public class SetReasonNoDesignerHandler : IRequestHandler<SetReasonNoDesignerRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetReasonNoDesignerHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetReasonNoDesignerRequest request, CancellationToken cancellationToken)
    {
        await UpdateLeadDesignerAppointed(request);
        return Unit.Value;
    }

    private async Task UpdateLeadDesignerAppointed(SetReasonNoDesignerRequest request)
    {
        await _progressReportingRepository.UpdateLeadDesignerNotAppointedReason(request.LeadDesignerNotAppointedReason, request.LeadDesignerNeedsSupport);
    }
}

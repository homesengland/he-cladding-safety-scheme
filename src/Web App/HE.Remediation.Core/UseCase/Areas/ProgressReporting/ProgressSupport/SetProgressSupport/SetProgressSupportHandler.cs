using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.SetProgressSupport;

public class SetProgressSupportHandler : IRequestHandler<SetProgressSupportRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetProgressSupportHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetProgressSupportRequest request, CancellationToken cancellationToken)
    {
        await _progressReportingRepository.UpdateProgressReportSupport(new UpdateProgressReportSupportParameters
        {
            SupportNeedsReason = request.SupportNeededReason,
            LeadDesignerNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.AppointingDesigner),
            OtherMembersNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.AppointingTeam),
            QuotesNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.SeekingQuotes),
            PlanningPermissionNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.PlanningPermission),
            OtherNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.Other)
        });

        return Unit.Value;
    }
}
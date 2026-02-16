using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport;
public class SetProgressSupportTypeHandler : IRequestHandler<SetProgressSupportTypeRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectSupportRepository _progressReportingProjectSupportRepository;

    public SetProgressSupportTypeHandler(IProgressReportingProjectSupportRepository progressReportingProjectSupportRepository, 
        IApplicationDataProvider applicationDataProvider)
    {
        _progressReportingProjectSupportRepository = progressReportingProjectSupportRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetProgressSupportTypeRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var parameters = new SetProjectSupportTypeParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            SupportNeedsReason = request.SupportNeededReason,
            LeadDesignerNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.AppointingDesigner),
            OtherMembersNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.AppointingTeam),
            QuotesNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.SeekingQuotes),
            PlanningPermissionNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.PlanningPermission),
            OtherNeedsSupport = request.SupportTypes.Contains(EProgressReportSupportType.Other)
        };
        await _progressReportingProjectSupportRepository.SetProgressReportSupportType(parameters);

        return Unit.Value;
    }
}

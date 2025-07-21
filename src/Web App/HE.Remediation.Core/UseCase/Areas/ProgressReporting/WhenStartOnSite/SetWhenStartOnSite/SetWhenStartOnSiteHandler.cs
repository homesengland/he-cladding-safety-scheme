using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.SetWhenStartOnSite;
    public class SetWhenStartOnSiteHandler : IRequestHandler<SetWhenStartOnSiteRequest, SetWhenStartOnSiteResponse>
    {
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetWhenStartOnSiteHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<SetWhenStartOnSiteResponse> Handle(SetWhenStartOnSiteRequest request, CancellationToken cancellationToken)
    {
        var expectedStartDateOnSite = new DateTime(request.StartYear!.Value, request.StartMonth!.Value, 1);

        await _progressReportingRepository.UpdateProgressReportExpectedStartDateOnSite(expectedStartDateOnSite);
        var leadDesignerNeedsSupport = await _progressReportingRepository.GetProgressReportLeadDesignerNeedsSupport();
        var otherMembersNeedsSupport = await _progressReportingRepository.GetProgressReportOtherMembersNeedsSupport();
        var quotesNeedsSupport = await _progressReportingRepository.GetProgressReportQuotesNeedsSupport();
        var planningPermissionNeedsSupport = await _progressReportingRepository.GetProgressReportPlanningPermissionNeedsSupport();

        return new SetWhenStartOnSiteResponse
        {
            NeedsSupport = leadDesignerNeedsSupport == true
                           || otherMembersNeedsSupport == true
                           || quotesNeedsSupport == true
                           || planningPermissionNeedsSupport == true
        };
    }
}


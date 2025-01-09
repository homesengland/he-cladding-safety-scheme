
using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;

public class SetWhenSubmitHandler : IRequestHandler<SetWhenSubmitRequest, SetWhenSubmitResponse>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetWhenSubmitHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<SetWhenSubmitResponse> Handle(SetWhenSubmitRequest request, CancellationToken cancellationToken)
    {
        await UpdateProgressReportExpectedWorksPackageSubmissionDate(request);

        var leadDesignerNeedsSupport = await _progressReportingRepository.GetProgressReportLeadDesignerNeedsSupport();
        var otherMembersNeedsSupport = await _progressReportingRepository.GetProgressReportOtherMembersNeedsSupport();
        var quotesNeedsSupport = await _progressReportingRepository.GetProgressReportQuotesNeedsSupport();
        var planningPermissionNeedsSupport = await _progressReportingRepository.GetProgressReportPlanningPermissionNeedsSupport();

        return new SetWhenSubmitResponse
        {
            NeedsSupport = (leadDesignerNeedsSupport != null && leadDesignerNeedsSupport.Value) 
                        || (otherMembersNeedsSupport != null && otherMembersNeedsSupport.Value)
                        || (quotesNeedsSupport != null && quotesNeedsSupport.Value)
                        || (planningPermissionNeedsSupport != null && planningPermissionNeedsSupport.Value)
        };
    }

    private async Task UpdateProgressReportExpectedWorksPackageSubmissionDate(SetWhenSubmitRequest request)
    {
        var submissionDate = request.SubmissionMonth is not null && request.SubmissionYear is not null
            ? new DateTime(request.SubmissionYear.Value, request.SubmissionMonth.Value, 1).AddMonths(1).AddDays(-1)
            : (DateTime?)null;

        await _progressReportingRepository.UpdateProgressReportExpectedWorksPackageSubmissionDate(submissionDate);
    }
}

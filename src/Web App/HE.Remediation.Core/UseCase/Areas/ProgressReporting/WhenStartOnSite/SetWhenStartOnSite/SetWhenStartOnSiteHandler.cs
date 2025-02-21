using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.SetWhenStartOnSite;
    public class SetWhenStartOnSiteHandler : IRequestHandler<SetWhenStartOnSiteRequest>
    {
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetWhenStartOnSiteHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetWhenStartOnSiteRequest request, CancellationToken cancellationToken)
    {
        await UpdateProgressReportExpectedStartDateOnSite(request);
        return Unit.Value;
    }

    private async Task UpdateProgressReportExpectedStartDateOnSite(SetWhenStartOnSiteRequest request)
    {
        var expectedStartDateOnSite = request.StartMonth is not null && request.StartYear is not null
            ? new DateTime(request.StartYear.Value, request.StartMonth.Value, 1).AddMonths(1).AddDays(-1)
            : (DateTime?)null;

        await _progressReportingRepository.UpdateProgressReportExpectedStartDateOnSite(expectedStartDateOnSite);
    }
}


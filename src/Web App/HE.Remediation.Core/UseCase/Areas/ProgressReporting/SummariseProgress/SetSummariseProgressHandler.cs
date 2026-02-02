using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SummariseProgress
{
    public class SetSummariseProgressHandler : IRequestHandler<SetSummariseProgressRequest>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;

        public SetSummariseProgressHandler(IProgressReportingRepository progressReportingRepository)
        {
            _progressReportingRepository = progressReportingRepository;
        }


        public async ValueTask<Unit> Handle(SetSummariseProgressRequest request, CancellationToken cancellationToken)
        {
            await UpdateSummariseProgress(request);
            return Unit.Value;
        }

        private async Task UpdateSummariseProgress(SetSummariseProgressRequest request)
        {
            await _progressReportingRepository.UpdateSummariseProgress(request);
        }
    }

    public class SetSummariseProgressRequest : IRequest
    {
        public string ProgressSummary { get; set; }
        public bool? IsSupportNeeded { get; set; }
    }
}

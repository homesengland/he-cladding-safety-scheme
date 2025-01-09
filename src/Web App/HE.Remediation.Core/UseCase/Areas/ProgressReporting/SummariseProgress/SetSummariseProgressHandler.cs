using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SummariseProgress
{
    public class SetSummariseProgressHandler : IRequestHandler<SetSummariseProgressRequest>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;

        public SetSummariseProgressHandler(IProgressReportingRepository progressReportingRepository)
        {
            _progressReportingRepository = progressReportingRepository;
        }


        public async Task<Unit> Handle(SetSummariseProgressRequest request, CancellationToken cancellationToken)
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
        public string GoalSummary { get; set; }
        public bool? IsSupportNeeded { get; set; }
    }
}

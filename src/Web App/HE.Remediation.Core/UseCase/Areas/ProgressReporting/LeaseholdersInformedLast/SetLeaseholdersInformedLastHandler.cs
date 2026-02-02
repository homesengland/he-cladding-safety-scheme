using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.SetInformedLeaseholder;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeaseholderInformedLast
{
    public class SetLeaseholdersInformedLastHandler : IRequestHandler<SetLeaseholdersInformedLastRequest>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;

        public SetLeaseholdersInformedLastHandler(IProgressReportingRepository progressReportingRepository)
        {
            _progressReportingRepository = progressReportingRepository;
        }


        public async ValueTask<Unit> Handle(SetLeaseholdersInformedLastRequest request, CancellationToken cancellationToken)
        {
            await UpdateLeaseholderInformedLastDate(request);
            return Unit.Value;
        }

        private async Task UpdateLeaseholderInformedLastDate(SetLeaseholdersInformedLastRequest request)
        {
            await _progressReportingRepository.UpdateLeaseholdersInformedLastDate(request.LeaseholdersInformedLastDate);
        }
    }

    public class SetLeaseholdersInformedLastRequest : IRequest
    {
        public DateTime? LeaseholdersInformedLastDate { get; set; }
    }
}

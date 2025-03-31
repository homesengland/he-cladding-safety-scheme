using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.SetWhenStartOnSite;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones.SetProjectPlanMilestones
{
    public class SetHasProjectPlanMilestonesHandler : IRequestHandler<SetHasProjectPlanMilestonesRequest>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;

        public SetHasProjectPlanMilestonesHandler(IProgressReportingRepository progressReportingRepository)
        {
            _progressReportingRepository = progressReportingRepository;
        }

        public async Task<Unit> Handle(SetHasProjectPlanMilestonesRequest request, CancellationToken cancellationToken)
        {
            await UpdateProgressReportHasProjectPlanMilestonesHandler(request);
            return Unit.Value;
        }

        private async Task UpdateProgressReportHasProjectPlanMilestonesHandler(SetHasProjectPlanMilestonesRequest request)
        {
            await _progressReportingRepository.UpdateProgressReportHasProjectPlanMilestones(request.HasProjectPlanMilestones);
        }
    }
}
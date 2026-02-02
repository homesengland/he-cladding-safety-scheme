using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.UpdateBuildingControlRequired
{
    public class UpdateBuildingControlRequiredHandler : IRequestHandler<UpdateBuildingControlRequiredRequest>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;

        public UpdateBuildingControlRequiredHandler(IProgressReportingRepository progressReportingRepository)
        {
            _progressReportingRepository = progressReportingRepository;
        }

        public async ValueTask<Unit> Handle(UpdateBuildingControlRequiredRequest request, CancellationToken cancellationToken)
        {
            await _progressReportingRepository.UpdateBuildingControlRequired(request.BuildingControlRequired);

            return Unit.Value;
        }
    }

    public class UpdateBuildingControlRequiredRequest : IRequest
    {
        public bool BuildingControlRequired { get; set; }
    }
}

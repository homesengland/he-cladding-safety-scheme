using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingControl.UpdateBuildingControlDetails
{
    public class UpdateBuildingControlDetailsHandler : IRequestHandler<UpdateBuildingControlDetailsRequest>
    {
        private readonly IProgressReportingRepository _progressReportingRepository;

        public UpdateBuildingControlDetailsHandler(IProgressReportingRepository progressReportingRepository)
        {
            _progressReportingRepository = progressReportingRepository;
        }

        public async Task<Unit> Handle(UpdateBuildingControlDetailsRequest request, CancellationToken cancellationToken)
        {
            var parameters = new ProgressReportUpdateBuildingControlDetails
            {
                ForecastDate = request.ForecastDateYear.HasValue && request.ForecastDateMonth.HasValue ? new DateTime(request.ForecastDateYear!.Value, request.ForecastDateMonth!.Value, 1) : null,
                ActualDate = request.ActualDateYear.HasValue && request.ActualDateMonth.HasValue ? new DateTime(request.ActualDateYear!.Value, request.ActualDateMonth!.Value, 1) : null,
                ValidationDate = request.ValidationDateYear.HasValue && request.ValidationDateMonth.HasValue ? new DateTime(request.ValidationDateYear!.Value, request.ValidationDateMonth!.Value, 1) : null,
                DecisionDate = request.DecisionDateYear.HasValue && request.DecisionDateMonth.HasValue ? new DateTime(request.DecisionDateYear!.Value, request.DecisionDateMonth!.Value, 1) : null,
                Decision = request.Decision
            };

            await _progressReportingRepository.UpdateBuildingControlDetails(parameters);

            return Unit.Value;
        }
    }

    public class UpdateBuildingControlDetailsRequest : IRequest
    {
        public int? ForecastDateMonth { get; set; }
        public int? ForecastDateYear { get; set; }
        public int? ActualDateMonth { get; set; }
        public int? ActualDateYear { get; set; }
        public int? ValidationDateMonth { get; set; }
        public int? ValidationDateYear { get; set; }
        public int? DecisionDateMonth { get; set; }
        public int? DecisionDateYear { get; set; }
        public bool? Decision { get; set; }
    }
}

using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl
{
    public class GetBuildingControlHandler : IRequestHandler<GetBuildingControlRequest, GetBuildingControlResponse>
    {
        private readonly IProgressReportingKeyDatesRepository _progressReportingKeyDatesRepository;
        private readonly IApplicationDetailsProvider _detailsProvider;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetBuildingControlHandler(IApplicationDetailsProvider detailsProvider, 
            IProgressReportingKeyDatesRepository progressReportingKeyDatesRepository,
            IApplicationDataProvider applicationDataProvider)
        {
            _detailsProvider = detailsProvider;
            _progressReportingKeyDatesRepository = progressReportingKeyDatesRepository;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetBuildingControlResponse> Handle(GetBuildingControlRequest request, CancellationToken cancellationToken)
        {
            var details = await _detailsProvider.GetApplicationDetails();
            var applicationId = _applicationDataProvider.GetApplicationId();
            var progressReportId = _applicationDataProvider.GetProgressReportId();

            var result = await _progressReportingKeyDatesRepository.GetBuildingControlKeyDates(
                                    new GetProgressReportBuildingControlKeyDatesParameters
                                    {
                                        ApplicationId = details.ApplicationId,
                                        ProgressReportId = progressReportId
                                    });

            return new GetBuildingControlResponse
            {
                ApplicationReferenceNumber = details.ApplicationReferenceNumber,
                BuildingName = details.BuildingName,
                BuildingControlExpectedApplicationDate = result?.BuildingControlExpectedApplicationDate,
                BuildingControlActualApplicationDate = result?.BuildingControlActualApplicationDate,
                BuildingControlValidationDate = result?.BuildingControlValidationDate,
                BuildingControlDecisionDate = result?.BuildingControlDecisionDate,
                PreviousBuildingControlExpectedApplicationDate = result?.PreviousBuildingControlExpectedApplicationDate,
                PreviousBuildingControlActualApplicationDate = result?.PreviousBuildingControlActualApplicationDate,
                PreviousBuildingControlValidationDate = result?.PreviousBuildingControlValidationDate,
                PreviousBuildingControlDecisionDate = result?.PreviousBuildingControlDecisionDate,
                Gateway2Reference = result?.Gateway2Reference,
                BuildingControlDecisionType = result?.BuildingControlDecisionTypeId
            };
        }
    }

    public class GetBuildingControlRequest() : IRequest<GetBuildingControlResponse>
    {
        public Guid MonthlyProgressReportId { get; set; }
    }

    public class GetBuildingControlResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public DateTime? PreviousBuildingControlExpectedApplicationDate { get; set; }
        public DateTime? PreviousBuildingControlActualApplicationDate { get; set; }
        public DateTime? PreviousBuildingControlValidationDate { get; set; }
        public DateTime? PreviousBuildingControlDecisionDate { get; set; }
        public DateTime? BuildingControlExpectedApplicationDate { get; set; }
        public DateTime? BuildingControlActualApplicationDate { get; set; }
        public DateTime? BuildingControlValidationDate { get; set; }
        public DateTime? BuildingControlDecisionDate { get; set; }
        public string Gateway2Reference { get; set; }
        public EBuildingControlDecisionType? BuildingControlDecisionType { get; set; }
    }
}

using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.Remediation;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.Remediation
{
    public class GetRemediationHandler : IRequestHandler<GetRemediationRequest, GetRemediationResponse>
    {
        private readonly IProgressReportingKeyDatesRepository _progressReportingKeyDatesRepository;
        private readonly IApplicationDetailsProvider _detailsProvider;

        public GetRemediationHandler(IApplicationDetailsProvider detailsProvider, IProgressReportingKeyDatesRepository progressReportingKeyDatesRepository)
        {
            _detailsProvider = detailsProvider;
            _progressReportingKeyDatesRepository = progressReportingKeyDatesRepository;
        }

        public async ValueTask<GetRemediationResponse> Handle(GetRemediationRequest request, CancellationToken cancellationToken)
        {
            var details = await _detailsProvider.GetApplicationDetails();

            var result = await _progressReportingKeyDatesRepository.GetRemediationKeyDates(
                                    new GetProgressReportRemediationKeyDatesParameters
                                    {
                                        ApplicationId = details.ApplicationId,
                                        ProgressReportId = request.MonthlyProgressReportId
                                    });

            return new GetRemediationResponse
            {
                ApplicationReferenceNumber = details.ApplicationReferenceNumber,
                BuildingName = details.BuildingName,
                PreviousFullCompletionOfWorksDate = result?.PreviousFullCompletionOfWorksDate,
                PreviousPracticalCompletionDate = result?.PreviousPracticalCompletionDate,
                FullCompletionOfWorksDate = result?.FullCompletionOfWorksDate,
                PracticalCompletionDate = result?.PracticalCompletionDate
            };
        }
    }

    public class GetRemediationRequest(Guid monthlyProgressReportId) : IRequest<GetRemediationResponse>
    {
        public Guid MonthlyProgressReportId { get; set; } = monthlyProgressReportId;
    }

    public class GetRemediationResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }

        public DateTime? PreviousFullCompletionOfWorksDate { get; set; }
        public DateTime? PreviousPracticalCompletionDate { get; set; }

        public DateTime? FullCompletionOfWorksDate { get; set; }
        public DateTime? PracticalCompletionDate { get; set; }

    }
}

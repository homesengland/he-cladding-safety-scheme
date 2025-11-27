using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.WorksPlanning;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;

public class GetWorksPlanningHandler : IRequestHandler<GetWorksPlanningRequest, GetWorksPlanningResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public GetWorksPlanningHandler(
        IApplicationDataProvider dataProvider, 
        IApplicationDetailsProvider detailsProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<GetWorksPlanningResponse> Handle(GetWorksPlanningRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();

        var worksPlanningDates = await _keyDatesRepository.GetProgressReportWorksPlanningKeyDates(
            new GetProgressReportWorksPlanningKeyDatesParameters
            {
                ApplicationId = details.ApplicationId,
                ProgressReportId = _dataProvider.GetProgressReportId()
            });

        return new GetWorksPlanningResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            ExpectedTenderMonth = worksPlanningDates?.ExpectedTenderDate.HasValue == true 
                ? worksPlanningDates.ExpectedTenderDate.Value.Month 
                : null,
            ExpectedTenderYear = worksPlanningDates?.ExpectedTenderDate.HasValue == true 
                ? worksPlanningDates.ExpectedTenderDate.Value.Year 
                : null,
            ExpectedLeadContractorAppointmentMonth = worksPlanningDates?.ExpectedLeadContractorAppointmentDate.HasValue == true 
                ? worksPlanningDates.ExpectedLeadContractorAppointmentDate.Value.Month 
                : null,
            ExpectedLeadContractorAppointmentYear = worksPlanningDates?.ExpectedLeadContractorAppointmentDate.HasValue == true 
                ? worksPlanningDates.ExpectedLeadContractorAppointmentDate.Value.Year 
                : null,
            ActualTenderMonth = worksPlanningDates?.ActualTenderDate.HasValue == true
                ? worksPlanningDates.ActualTenderDate.Value.Month
                : null,
            ActualTenderYear = worksPlanningDates?.ActualTenderDate.HasValue == true
                ? worksPlanningDates.ActualTenderDate.Value.Year
                : null,
            ActualLeadContractorAppointmentMonth = worksPlanningDates?.ActualLeadContractAppointmentDate.HasValue == true
                ? worksPlanningDates.ActualLeadContractAppointmentDate.Value.Month
                : null,
            ActualLeadContractorAppointmentYear = worksPlanningDates?.ActualLeadContractAppointmentDate.HasValue == true
                ? worksPlanningDates.ActualLeadContractAppointmentDate.Value.Year
                : null,
            ExpectedWorksPackageSubmissionMonth = worksPlanningDates?.ExpectedWorksPackageSubmissionDate.HasValue == true
                ? worksPlanningDates.ExpectedWorksPackageSubmissionDate.Value.Month
                : null,
            ExpectedWorksPackageSubmissionYear = worksPlanningDates?.ExpectedWorksPackageSubmissionDate.HasValue == true
                ? worksPlanningDates.ExpectedWorksPackageSubmissionDate.Value.Year
                : null,
            PreviousExpectedTenderDate = worksPlanningDates?.PreviousTenderDate,
            PreviousExpectedLeadContractorAppointmentDate = worksPlanningDates?.PreviousContractorAppointmentDate,
            PreviousActualTenderDate = worksPlanningDates?.PreviousActualTenderDate,
            PreviousActualLeadContractorAppointmentDate = worksPlanningDates?.PreviousActualContractorAppointmentDate,
            PreviousExpectedWorksPackageSubmissionDate = worksPlanningDates?.PreviousExpectedWorksPackageSubmissionDate
        };
    }
}

public class GetWorksPlanningRequest : IRequest<GetWorksPlanningResponse>
{
    private GetWorksPlanningRequest()
    {
    }

    public static readonly GetWorksPlanningRequest Request = new();
}

public class GetWorksPlanningResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? ExpectedTenderMonth { get; set; }
    public int? ExpectedTenderYear { get; set; }
    public int? ActualTenderMonth { get; set; }
    public int? ActualTenderYear { get; set; }
    public int? ExpectedLeadContractorAppointmentMonth { get; set; }
    public int? ExpectedLeadContractorAppointmentYear { get; set; }
    public int? ActualLeadContractorAppointmentMonth { get; set; }
    public int? ActualLeadContractorAppointmentYear { get; set; }
    public int? ExpectedWorksPackageSubmissionMonth { get; set; }
    public int? ExpectedWorksPackageSubmissionYear { get; set; }
    public DateTime? PreviousExpectedTenderDate { get; set; }
    public DateTime? PreviousExpectedLeadContractorAppointmentDate { get; set; }
    public DateTime? PreviousActualTenderDate { get; set; }
    public DateTime? PreviousActualLeadContractorAppointmentDate { get; set; }
    public DateTime? PreviousExpectedWorksPackageSubmissionDate { get; set; }
}
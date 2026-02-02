using System.Transactions;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.WorksPlanning;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;

public class SetWorksPlanningHandler : IRequestHandler<SetWorksPlanningRequest, SetWorksPlanningResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetWorksPlanningHandler(IApplicationDataProvider applicationDataProvider, IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<SetWorksPlanningResponse> Handle(SetWorksPlanningRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var keyDates = await _keyDatesRepository.GetProgressReportWorksPlanningKeyDates(
            new GetProgressReportWorksPlanningKeyDatesParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _keyDatesRepository.SetProgressReportWorksPlanningKeyDates(
            new SetProgressReportWorksPlanningKeyDatesParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                ExpectedTenderDate = request.ExpectedTenderDate,
                ExpectedLeadContractorDate = request.ExpectedLeadContractorAppointmentDate,
                ActualTenderDate = request.ActualTenderDate,
                ActualLeadContractorDate = request.ActualLeadContractorAppointmentDate,
                ExpectedWorksPackageSubmissionDate = request.ExpectedWorksPackageSubmissionDate
            });

        var expectedTenderDateChanged = keyDates.PreviousTenderDate.HasChanged(request.ExpectedTenderDate);
        var expectedLeadContractorAppointmentDateChanged = keyDates.PreviousContractorAppointmentDate.HasChanged(request.ExpectedLeadContractorAppointmentDate); 
        var actualTenderDateChanged = keyDates.PreviousActualTenderDate.HasChanged(request.ActualTenderDate);
        var actualLeadContractorAppointmentDateChanged = keyDates.PreviousActualContractorAppointmentDate.HasChanged(request.ActualLeadContractorAppointmentDate);
        var expectedWorksPackageSubmissionDateChanged = keyDates.PreviousExpectedWorksPackageSubmissionDate.HasChanged(request.ExpectedWorksPackageSubmissionDate);

        var hasChangedDates = expectedTenderDateChanged ||
                              expectedLeadContractorAppointmentDateChanged ||
                              actualTenderDateChanged ||
                              actualLeadContractorAppointmentDateChanged ||
                              expectedWorksPackageSubmissionDateChanged;

        if (!hasChangedDates)
        {
            // no dates have changed, clear reason
            await _keyDatesRepository.SetWorksPlanningDatesChanged(new SetWorksPlanningDatesChangedParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                DatesChangedReason = null,
                DatesChangedTypeId = null
            });
        }

        scope.Complete();

        return new SetWorksPlanningResponse
        {
            HasChangedDates = hasChangedDates
        };
    }
}

public class SetWorksPlanningRequest : IRequest<SetWorksPlanningResponse>
{
    public DateTime? ExpectedTenderDate { get; set; }
    public DateTime? ActualTenderDate { get; set; }
    public DateTime? ExpectedLeadContractorAppointmentDate { get; set; }
    public DateTime? ActualLeadContractorAppointmentDate { get; set; }
    public DateTime? ExpectedWorksPackageSubmissionDate { get; set; }
}

public class SetWorksPlanningResponse
{
    public bool HasChangedDates { get; set; }
}
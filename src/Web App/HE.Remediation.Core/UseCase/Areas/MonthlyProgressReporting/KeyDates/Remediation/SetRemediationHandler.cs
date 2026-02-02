using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.Remediation;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.Remediation;

public class SetRemediationHandler : IRequestHandler<SetRemediationRequest, SetRemediationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetRemediationHandler(IApplicationDataProvider applicationDataProvider, IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<SetRemediationResponse> Handle(SetRemediationRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var keyDates = await _keyDatesRepository.GetRemediationKeyDates(
            new GetProgressReportRemediationKeyDatesParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _keyDatesRepository.SetRemediationKeyDates(
            new SetProgressReportRemediationKeyDatesParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                FullCompletionOfWorksDate = request.FullCompletionOfWorksDate,
                PracticalCompletionDate = request.PracticalCompletionDate
            });

        var fullCompletionOfWorksDateChanged = keyDates.PreviousFullCompletionOfWorksDate.HasChanged(request.FullCompletionOfWorksDate);
        var practicalCompletionDateChanged = keyDates.PreviousPracticalCompletionDate.HasChanged(request.PracticalCompletionDate);

        var hasChangedDates = fullCompletionOfWorksDateChanged ||
                              practicalCompletionDateChanged;

        if (!hasChangedDates)
        {
            await _keyDatesRepository.SetRemediationDatesChanged(new SetRemediationDatesChangedParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                DatesChangedReason = null,
                DatesChangedTypeId = null
            });
        }

        scope.Complete();

        return new SetRemediationResponse
        {
            HasChangedDates = hasChangedDates
        };
    }
}

public class SetRemediationRequest : IRequest<SetRemediationResponse>
{
    public DateTime FullCompletionOfWorksDate { get; set; }
    public DateTime PracticalCompletionDate { get; set; }
}

public class SetRemediationResponse
{
    public bool HasChangedDates { get; set; }
}
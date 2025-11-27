using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.Remediation;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.Remediation;

public class SetRemediationDatesChangedHandler : IRequestHandler<SetRemediationDatesChangedRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetRemediationDatesChangedHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<Unit> Handle(SetRemediationDatesChangedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        await _keyDatesRepository.SetRemediationDatesChanged(new SetRemediationDatesChangedParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            DatesChangedTypeId = request.ChangeTypeId!.Value,
            DatesChangedReason = request.ChangeReason
        });

        return Unit.Value;
    }
}

public class SetRemediationDatesChangedRequest : IRequest
{
    public int? ChangeTypeId { get; set; }
    public string ChangeReason { get; set; }
}
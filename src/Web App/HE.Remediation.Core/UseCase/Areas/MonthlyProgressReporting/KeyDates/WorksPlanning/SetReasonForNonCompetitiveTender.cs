using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.WorksPlanning;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;
public class SetReasonForNonCompetitiveTenderHandler : IRequestHandler<SetReasonForNonCompetitiveTenderRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetReasonForNonCompetitiveTenderHandler(IApplicationDataProvider applicationDataProvider, IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<Unit> Handle(SetReasonForNonCompetitiveTenderRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        await _keyDatesRepository.SetReasonForNonCompetitiveTender(
            new SetReasonForNonCompetitiveTenderParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                ReasonForNonCompetitiveTender = request.ReasonForNonCompetitiveTender
            });
        return Unit.Value;
    }
}

public class SetReasonForNonCompetitiveTenderRequest : IRequest<Unit>
{
    public string ReasonForNonCompetitiveTender { get; set; }
}

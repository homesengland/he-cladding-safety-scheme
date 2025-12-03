using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.WorksPlanning;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;

public class SetWorksPlanningDatesChangedHandler : IRequestHandler<SetWorksPlanningDatesChangedRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetWorksPlanningDatesChangedHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<Unit> Handle(SetWorksPlanningDatesChangedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        await _keyDatesRepository.SetWorksPlanningDatesChanged(new SetWorksPlanningDatesChangedParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            DatesChangedTypeId = request.ChangeTypeId!.Value,
            DatesChangedReason = request.ChangeReason
        });

        return Unit.Value;
    }
}

public class SetWorksPlanningDatesChangedRequest : IRequest
{
    public int? ChangeTypeId { get; set; }
    public string ChangeReason { get; set; }
}
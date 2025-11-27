using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;

public class SetBuildingControlDatesChangedHandler : IRequestHandler<SetBuildingControlDatesChangedRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetBuildingControlDatesChangedHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<Unit> Handle(SetBuildingControlDatesChangedRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        await _keyDatesRepository.SetBuildingControlDatesChanged(new SetBuildingControlDatesChangedParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            DatesChangedTypeId = request.ChangeTypeId!.Value,
            DatesChangedReason = request.ChangeReason
        });

        return Unit.Value;
    }
}

public class SetBuildingControlDatesChangedRequest : IRequest
{
    public int? ChangeTypeId { get; set; }
    public string ChangeReason { get; set; }
}
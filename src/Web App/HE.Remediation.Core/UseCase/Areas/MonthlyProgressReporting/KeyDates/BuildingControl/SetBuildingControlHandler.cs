using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.BuildingControl;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.BuildingControl;

public class SetBuildingControlHandler : IRequestHandler<SetBuildingControlRequest, SetBuildingControlResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetBuildingControlHandler(IApplicationDataProvider applicationDataProvider, IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async ValueTask<SetBuildingControlResponse> Handle(SetBuildingControlRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var keyDates = await _keyDatesRepository.GetBuildingControlKeyDates(
           new GetProgressReportBuildingControlKeyDatesParameters
           {
               ApplicationId = applicationId,
               ProgressReportId = progressReportId
           });

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _keyDatesRepository.SetBuildingControlKeyDates(
            new SetProgressReportBuildingControlKeyDatesParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                BuildingControlExpectedApplicationDate = request.BuildingControlExpectedApplicationDate,
                BuildingControlActualApplicationDate = request.BuildingControlActualApplicationDate,
                BuildingControlValidationDate = request.BuildingControlValidationDate,
                BuildingControlDecisionDate = request.BuildingControlDecisionDate,
                Gateway2Reference = request.Gateway2Reference,
                BuildingControlDecisionTypeId = (int?)request.BuildingControlDecisionType
            });

        var buildingControlExpectedApplicationDateChanged = keyDates.PreviousBuildingControlExpectedApplicationDate.HasChanged(request.BuildingControlExpectedApplicationDate);
        var buildingControlActualApplicationDateChanged = keyDates.PreviousBuildingControlActualApplicationDate.HasChanged(request.BuildingControlActualApplicationDate);
        var buildingControlValidationDateChanged = keyDates.PreviousBuildingControlValidationDate.HasChanged(request.BuildingControlValidationDate);
        var buildingControlDecisionDateChanged = keyDates.PreviousBuildingControlDecisionDate.HasChanged(request.BuildingControlDecisionDate);

        var hasChangedDates = buildingControlExpectedApplicationDateChanged ||
                              buildingControlActualApplicationDateChanged ||
                              buildingControlValidationDateChanged ||
                              buildingControlDecisionDateChanged;

        if (!hasChangedDates)
        {
            await _keyDatesRepository.SetBuildingControlDatesChanged(new SetBuildingControlDatesChangedParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                DatesChangedReason = null,
                DatesChangedTypeId = null
            });
        }

        scope.Complete();

        return new SetBuildingControlResponse
        {
            HasChangedDates = hasChangedDates
        };
    }
}

public class SetBuildingControlRequest : IRequest<SetBuildingControlResponse>
{
    public DateTime? BuildingControlExpectedApplicationDate { get; set; }
    public DateTime? BuildingControlActualApplicationDate { get; set; }
    public DateTime? BuildingControlValidationDate { get; set; }
    public DateTime? BuildingControlDecisionDate { get; set; }
    public string Gateway2Reference { get; set; }
    public EBuildingControlDecisionType? BuildingControlDecisionType { get; set; }
}

public class SetBuildingControlResponse
{
    public bool HasChangedDates { get; set; }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetHasAppliedForBuildingControlHandler : IRequestHandler<SetHasAppliedForBuildingControlRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetHasAppliedForBuildingControlHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetHasAppliedForBuildingControlRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _progressReportingRepository.UpdateHasAppliedForBuildingControl(
            new UpdateHasAppliedForBuildingControlParameters
            {
                ApplicationId = _applicationDataProvider.GetApplicationId(),
                ProgressReportId = _applicationDataProvider.GetProgressReportId(),
                HasAppliedForBuildingControl = request.HasAppliedForBuildingControl!.Value
            });

        return Unit.Value;
    }
}

public class SetHasAppliedForBuildingControlRequest : IRequest
{
    public bool? HasAppliedForBuildingControl { get; set; }
}
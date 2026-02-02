using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetBuildingControlForecastHandler : IRequestHandler<SetBuildingControlForecastRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetBuildingControlForecastHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetBuildingControlForecastRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _progressReportingRepository.UpdateBuildingControlForecast(new UpdateBuildingControlForecastParameters
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            ProgressReportId = _applicationDataProvider.GetProgressReportId(),
            ForecastDate = request.ForecastDate,
            ForecastInformation = request.ForecastInformation
        });

        return Unit.Value;
    }
}

public class SetBuildingControlForecastRequest : IRequest
{
    public DateTime? ForecastDate { get; set; }
    public string ForecastInformation { get; set; }
}
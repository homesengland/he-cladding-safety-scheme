using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetBuildingControlForecastHandler : IRequestHandler<GetBuildingControlForecastRequest, GetBuildingControlForecastResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetBuildingControlForecastHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<GetBuildingControlForecastResponse> Handle(GetBuildingControlForecastRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var forecast = await _progressReportingRepository.GetBuildingControlForecast(
            new GetBuildingControlForecastParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        var version = await _progressReportingRepository.GetProgressReportVersion();

        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return new GetBuildingControlForecastResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            BuildingControlRequired = forecast.BuildingControlRequired,
            ForecastDateMonth = forecast.BuildingControlForecastSubmissionDate?.Month,
            ForecastDateYear = forecast.BuildingControlForecastSubmissionDate?.Year,
            ForecastInformation = forecast.BuildingControlForecastInformation,
            Version = version,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}

public class GetBuildingControlForecastRequest : IRequest<GetBuildingControlForecastResponse>
{
    private GetBuildingControlForecastRequest()
    {
    }

    public static readonly GetBuildingControlForecastRequest Request = new();
}

public class GetBuildingControlForecastResponse
{
    public bool? BuildingControlRequired { get; set; }
    public int? ForecastDateMonth { get; set; }
    public int? ForecastDateYear { get; set; }
    public string ForecastInformation { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetBuildingControlValidationHandler : IRequestHandler<GetBuildingControlValidationRequest, GetBuildingControlValidationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetBuildingControlValidationHandler(
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

    public async Task<GetBuildingControlValidationResponse> Handle(GetBuildingControlValidationRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var validation = await _progressReportingRepository.GetBuildingControlValidation(
            new GetBuildingControlValidationParameters
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

        return new GetBuildingControlValidationResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            BuildingControlRequired = validation.BuildingControlRequired,
            ValidationDateMonth = validation.BuildingControlValidationDate?.Month,
            ValidationDateYear = validation.BuildingControlValidationDate?.Year,
            ValidationInformation = validation.BuildingControlValidationInformation,
            Version = version,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}

public class GetBuildingControlValidationRequest : IRequest<GetBuildingControlValidationResponse>
{
    private GetBuildingControlValidationRequest()
    {
    }

    public static readonly GetBuildingControlValidationRequest Request = new();
}

public class GetBuildingControlValidationResponse
{
    public bool? BuildingControlRequired { get; set; }
    public int? ValidationDateMonth { get; set; }
    public int? ValidationDateYear { get; set; }
    public string ValidationInformation { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
}
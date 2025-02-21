using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetBuildingControlSubmissionHandler : IRequestHandler<GetBuildingControlSubmissionRequest, GetBuildingControlSubmissionResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetBuildingControlSubmissionHandler(
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

    public async Task<GetBuildingControlSubmissionResponse> Handle(GetBuildingControlSubmissionRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var submission = await _progressReportingRepository.GetBuildingControlSubmission(
            new GetBuildingControlSubmissionParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        var version = await _progressReportingRepository.GetProgressReportVersion();

        return new GetBuildingControlSubmissionResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            BuildingControlRequired = submission.BuildingControlRequired,
            BuildingControlApplicationReference = submission.BuildingControlApplicationReference,
            SubmissionDateMonth = submission.BuildingControlActualSubmissionDate?.Month,
            SubmissionDateYear = submission.BuildingControlActualSubmissionDate?.Year,
            SubmissionInformation = submission.BuildingControlActualSubmissionInformation,
            Version = version
        };
    }
}

public class GetBuildingControlSubmissionRequest : IRequest<GetBuildingControlSubmissionResponse>
{
    private GetBuildingControlSubmissionRequest()
    {
    }

    public static readonly GetBuildingControlSubmissionRequest Request = new();
}

public class GetBuildingControlSubmissionResponse
{
    public bool? BuildingControlRequired { get; set; }

    public int? SubmissionDateMonth { get; set; }
    public int? SubmissionDateYear { get; set; }
    public string SubmissionInformation { get; set; }
    public string BuildingControlApplicationReference { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
}
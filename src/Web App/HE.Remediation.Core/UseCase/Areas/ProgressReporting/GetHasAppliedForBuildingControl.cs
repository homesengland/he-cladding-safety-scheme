using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetHasAppliedForBuildingControlHandler : IRequestHandler<GetHasAppliedForBuildingControlRequest, GetHasAppliedForBuildingControlResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetHasAppliedForBuildingControlHandler(
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

    public async Task<GetHasAppliedForBuildingControlResponse> Handle(GetHasAppliedForBuildingControlRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReport = _applicationDataProvider.GetProgressReportId();
        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var version = await _progressReportingRepository.GetProgressReportVersion();

        var hasAppliedForBuildingControl = await _progressReportingRepository.GetHasAppliedForBuildingControl(
            new GetHasAppliedForBuildingControlParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReport
            });

        return new GetHasAppliedForBuildingControlResponse
        {
            BuildingControlRequired = hasAppliedForBuildingControl.BuildingControlRequired,
            HasAppliedForBuildingControl = hasAppliedForBuildingControl.HasAppliedForBuildingControl,
            BuildingName = buildingName,
            ApplicationReferenceNumber = referenceNumber,
            Version = version
        };
    }
}

public class GetHasAppliedForBuildingControlRequest : IRequest<GetHasAppliedForBuildingControlResponse>
{
    private GetHasAppliedForBuildingControlRequest()
    {
    }

    public static readonly GetHasAppliedForBuildingControlRequest Request = new();
}

public class GetHasAppliedForBuildingControlResponse
{
    public bool? BuildingControlRequired { get; set; }
    public bool? HasAppliedForBuildingControl { get; set; }

    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public int Version { get; set; }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressReports.GetProgressReports;

public class GetProgressReportsHandler : IRequestHandler<GetProgressReportsRequest, GetProgressReportsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetProgressReportsHandler(IApplicationDataProvider applicationDataProvider,
                                     IBuildingDetailsRepository buildingDetailsRepository,
                                     IApplicationRepository applicationRepository,
                                     IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
    }

    public async Task<GetProgressReportsResponse> Handle(GetProgressReportsRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);
        var progressReports = await _progressReportingRepository.GetProgressReports();

        return new GetProgressReportsResponse
        {
            ApplicationReferenceNumber = applicationReferenceNumber,
            BuildingName = buildingName,
            ProgressReports = progressReports
        };
    }
}

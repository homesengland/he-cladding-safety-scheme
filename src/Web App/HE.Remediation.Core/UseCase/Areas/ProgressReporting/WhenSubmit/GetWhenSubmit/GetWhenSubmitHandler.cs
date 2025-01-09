using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.GetWhenSubmit;

public class GetWhenSubmitHandler : IRequestHandler<GetWhenSubmitRequest, GetWhenSubmitResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetWhenSubmitHandler(IApplicationDataProvider applicationDataProvider,
                                IBuildingDetailsRepository buildingDetailsRepository,
                                IApplicationRepository applicationRepository,
                                IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetWhenSubmitResponse> Handle(GetWhenSubmitRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var submissionDate = await _progressReportingRepository.GetProgressReportExpectedWorksPackageSubmissionDate();
        var buildingControlRequired = await _progressReportingRepository.GetBuildingControlRequired();

        var version = await _progressReportingRepository.GetProgressReportVersion();

        return new GetWhenSubmitResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            BuildingControlRequired = buildingControlRequired,
            SubmissionMonth = submissionDate?.Month,
            SubmissionYear = submissionDate?.Year,
            Version = version
        };
    }
}

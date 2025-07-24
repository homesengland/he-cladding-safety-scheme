using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;


namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenStartOnSite.GetWhenStartOnSite;

public class GetStartOnSiteHandler : IRequestHandler<GetWhenStartOnSiteRequest, GetWhenStartOnSiteResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetStartOnSiteHandler(IApplicationDataProvider applicationDataProvider,
                                IBuildingDetailsRepository buildingDetailsRepository,
                                IApplicationRepository applicationRepository,
                                IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetWhenStartOnSiteResponse> Handle(GetWhenStartOnSiteRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var startDateOnSite = await _progressReportingRepository.GetProgressReportExpectedStartDateOnSite();
        var buildingControlRequired = await _progressReportingRepository.GetBuildingControlRequired();

        var version = await _progressReportingRepository.GetProgressReportVersion();

        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        return new GetWhenStartOnSiteResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            StartMonth = startDateOnSite?.Month,
            StartYear = startDateOnSite?.Year,
            Version = version,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}


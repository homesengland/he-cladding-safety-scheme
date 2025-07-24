using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.GetInformedLeaseholder;

public class GetInformedLeaseholderHandler : IRequestHandler<GetInformedLeaseholderRequest, GetInformedLeaseholderResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetInformedLeaseholderHandler(IApplicationDataProvider applicationDataProvider,
                                         IBuildingDetailsRepository buildingDetailsRepository,
                                         IApplicationRepository applicationRepository,
                                         IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetInformedLeaseholderResponse> Handle(GetInformedLeaseholderRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        
        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var leaseholdersInformed = await _progressReportingRepository.GetProgressReportLeaseholdersInformed();
        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        return new GetInformedLeaseholderResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            LeaseholdersInformed = leaseholdersInformed,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}

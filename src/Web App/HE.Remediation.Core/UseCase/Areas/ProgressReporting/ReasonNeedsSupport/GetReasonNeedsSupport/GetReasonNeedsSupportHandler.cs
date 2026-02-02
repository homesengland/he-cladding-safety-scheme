using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.GetReasonNeedsSupport;

public class GetReasonNeedsSupportHandler : IRequestHandler<GetReasonNeedsSupportRequest, GetReasonNeedsSupportResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetReasonNeedsSupportHandler(IApplicationDataProvider applicationDataProvider,
                                      IBuildingDetailsRepository buildingDetailsRepository,
                                      IApplicationRepository applicationRepository,
                                      IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<GetReasonNeedsSupportResponse> Handle(GetReasonNeedsSupportRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var leadDesignerNeedsSupport = await _progressReportingRepository.GetProgressReportLeadDesignerNeedsSupport();
        var otherMembersNeedsSupport = await _progressReportingRepository.GetProgressReportOtherMembersNeedsSupport();
        var planningPermissionNeedsSupport = await _progressReportingRepository.GetProgressReportPlanningPermissionNeedsSupport();
        var quotesNeedsSupport = await _progressReportingRepository.GetProgressReportQuotesNeedsSupport();

        var supportNeededReason = await _progressReportingRepository.GetProgressReportSupportNeededReason();

        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        return new GetReasonNeedsSupportResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            LeadDesignerNeedsSupport = leadDesignerNeedsSupport,
            OtherMembersNeedsSupport = otherMembersNeedsSupport,
            PlanningPermissionNeedsSupport = planningPermissionNeedsSupport,
            QuotesNeedsSupport = quotesNeedsSupport,
            SupportNeededReason = supportNeededReason,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}

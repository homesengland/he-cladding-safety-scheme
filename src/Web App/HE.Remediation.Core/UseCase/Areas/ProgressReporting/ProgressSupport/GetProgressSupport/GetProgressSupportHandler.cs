using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressSupport.GetProgressSupport;

public class GetProgressSupportHandler : IRequestHandler<GetProgressSupportRequest, GetProgressSupportResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetProgressSupportHandler(
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

    public async Task<GetProgressSupportResponse> Handle(GetProgressSupportRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var support = await _progressReportingRepository.GetProgressReportSupport();

        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });
        
        var response = new GetProgressSupportResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            SupportNeededReason = support?.SupportNeededReason,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };

        if (support?.LeadDesignerNeedsSupport == true)
        {
            response.SupportTypes.Add(EProgressReportSupportType.AppointingDesigner);
        }

        if (support?.OtherMembersNeedsSupport == true)
        {
            response.SupportTypes.Add(EProgressReportSupportType.AppointingTeam);
        }

        if (support?.QuotesNeedsSupport == true)
        {
            response.SupportTypes.Add(EProgressReportSupportType.SeekingQuotes);
        }

        if (support?.PlanningPermissionNeedsSupport == true)
        {
            response.SupportTypes.Add(EProgressReportSupportType.PlanningPermission);
        }

        if (support?.OtherNeedsSupport == true)
        {
            response.SupportTypes.Add(EProgressReportSupportType.Other);
        }

        return response;
    }
}
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport;

public class GetProgressSupportTypeHandler : IRequestHandler<GetProgressSupportTypeRequest, GetProgressSupportTypeResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectSupportRepository _progressReportingProjectSupportRepository;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;

    public GetProgressSupportTypeHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectSupportRepository progressReportingProjectSupport,
        IApplicationDetailsProvider applicationDetailsProvider)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectSupportRepository = progressReportingProjectSupport;
        _applicationDetailsProvider = applicationDetailsProvider;
    }

    public async ValueTask<GetProgressSupportTypeResponse> Handle(GetProgressSupportTypeRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var parameters = new GetProgressReportSupportTypeParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = _applicationDataProvider.GetProgressReportId()
        };

        var support = await _progressReportingProjectSupportRepository.GetProgressReportSupportType(parameters);

        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var reference = applicationDetails.ApplicationReferenceNumber;
        var buildingName = applicationDetails.BuildingName;

        var supportTypes = new List<EProgressReportSupportType>();
        if (support?.LeadDesignerNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.AppointingDesigner);
        }
        if (support?.OtherMembersNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.AppointingTeam);
        }
        if (support?.QuotesNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.SeekingQuotes);
        }
        if (support?.PlanningPermissionNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.PlanningPermission);
        }
        if (support?.OtherNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.Other);
        }

        var response = new GetProgressSupportTypeResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,
            SupportNeededReason = support?.SupportNeededReason,
            SupportTypes = supportTypes
        };

        return response;
    }
}

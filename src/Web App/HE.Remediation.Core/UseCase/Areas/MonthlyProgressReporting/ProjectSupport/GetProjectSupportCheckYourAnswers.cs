using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectSupport;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectSupport;

public class GetProjectSupportCheckYourAnswers : IRequestHandler<GetProjectSupportCheckYourAnswersRequest, GetProjectSupportCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IProgressReportingProjectSupportRepository _projectSupportRepository;

    public GetProjectSupportCheckYourAnswers(IApplicationDataProvider applicationDataProvider, IApplicationDetailsProvider applicationDetailsProvider, IProgressReportingProjectSupportRepository projectSupportRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationDetailsProvider = applicationDetailsProvider;
        _projectSupportRepository = projectSupportRepository;
    }

    public async ValueTask<GetProjectSupportCheckYourAnswersResponse> Handle(GetProjectSupportCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();

        var checkYourAnswers = await _projectSupportRepository.GetProjectSupportCheckYourAnswers(
            new GetProjectSupportCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        var supportTypes = new List<EProgressReportSupportType>();
        if (checkYourAnswers?.LeadDesignerNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.AppointingDesigner);
        }
        if (checkYourAnswers?.OtherMembersNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.AppointingTeam);
        }
        if (checkYourAnswers?.QuotesNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.SeekingQuotes);
        }
        if (checkYourAnswers?.PlanningPermissionNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.PlanningPermission);
        }
        if (checkYourAnswers?.OtherNeedsSupport == true)
        {
            supportTypes.Add(EProgressReportSupportType.Other);
        }

        return new GetProjectSupportCheckYourAnswersResponse
        {
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            BuildingName = applicationDetails.BuildingName,
            RequiresSupport = checkYourAnswers.RequiresSupport,
            SupportTypes = supportTypes,
            SupportNeededReason = checkYourAnswers?.SupportNeededReason
        };
    }
}

public class GetProjectSupportCheckYourAnswersRequest : IRequest<GetProjectSupportCheckYourAnswersResponse>
{
    private GetProjectSupportCheckYourAnswersRequest()
    {
    }

    public static readonly GetProjectSupportCheckYourAnswersRequest Request = new();
}

public class GetProjectSupportCheckYourAnswersResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public bool? RequiresSupport { get; set; }
    public IList<EProgressReportSupportType> SupportTypes { get; set; }
    public string SupportNeededReason { get; set; }
    public GetProjectSupportCheckYourAnswersResponse()
    {
        SupportTypes = new List<EProgressReportSupportType>();
    }
}
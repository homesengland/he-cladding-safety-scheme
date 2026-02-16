using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class ProjectTeamContinueHandler : IRequestHandler<ProjectTeamContinueRequest, ProjectTeamContinueResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public ProjectTeamContinueHandler(
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<ProjectTeamContinueResponse> Handle(ProjectTeamContinueRequest request,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var teamMembers = await _progressReportingRepository.GetTeamMembers();

        var hasCertifyingOfficerRoles = teamMembers.Any(x => x.RoleId == (int)ETeamRole.ProjectManager) &&
                                        teamMembers.Any(x => x.RoleId == (int)ETeamRole.QuantitySurveyor);

        var isGcoComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();

        var hasVisitedCheckYourAnswers =
            await _progressReportingRepository.GetHasVisitedCheckYourAnswers(new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = _applicationDataProvider.GetApplicationId(),
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        var version = await _progressReportingRepository.GetProgressReportVersion();

        return new ProjectTeamContinueResponse
        {
            HasCertifyingOfficerRoles = hasCertifyingOfficerRoles,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers,
            IsGcoComplete = isGcoComplete,
            Version = version
        };
    }
}

public class ProjectTeamContinueRequest : IRequest<ProjectTeamContinueResponse>
{
    private ProjectTeamContinueRequest()
    {
    }

    public static readonly ProjectTeamContinueRequest Request = new();
}

public class ProjectTeamContinueResponse
{
    public bool HasCertifyingOfficerRoles { get; set; }
    public bool HasVisitedCheckYourAnswers { get; set; }
    public bool IsGcoComplete { get; set; }
    public int Version { get; set; }
}
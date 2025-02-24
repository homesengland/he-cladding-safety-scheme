using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class ProjectTeamContinueHandler : IRequestHandler<ProjectTeamContinueRequest, ProjectTeamContinueResponse>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public ProjectTeamContinueHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<ProjectTeamContinueResponse> Handle(ProjectTeamContinueRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var teamMembers = await _progressReportingRepository.GetTeamMembers();

        var hasCertifyingOfficerRoles = teamMembers.Any(x => x.RoleId == (int)ETeamRole.ProjectManager) &&
                                        teamMembers.Any(x => x.RoleId == (int)ETeamRole.QuantitySurveyor);

        return new ProjectTeamContinueResponse
        {
            HasCertifyingOfficerRoles = hasCertifyingOfficerRoles
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
}
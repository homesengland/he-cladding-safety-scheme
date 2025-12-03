using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class GetWhoIsTheGrantCertifyingOfficerHandler : IRequestHandler<GetWhoIsTheGrantCertifyingOfficerRequest, GetWhoIsTheGrantCertifyingOfficerResponse>
{
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public GetWhoIsTheGrantCertifyingOfficerHandler(
        IApplicationDetailsProvider detailsProvider,
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _detailsProvider = detailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<GetWhoIsTheGrantCertifyingOfficerResponse> Handle(GetWhoIsTheGrantCertifyingOfficerRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var gcoDetails = await _progressReportingProjectTeamRepository.GetGrantCertifyingOfficer(progressReportId);
        var gcoTeamMemberId = gcoDetails.TeamMemberId;

        var applicableTeamMembers = await GetApplicableTeamMembers(details.ApplicationId, progressReportId);

        return new GetWhoIsTheGrantCertifyingOfficerResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            ProjectTeamMemberId = gcoTeamMemberId,
            TeamMembers = [.. applicableTeamMembers.Select(x => new GetWhoIsTheGrantCertifyingOfficerResponse.TeamMemberResponse
            {
                Id = x.Id,
                Name = x.Name,
                Role = x.RoleName
            })]
        };
    }

    private async Task<List<GetTeamMembersResult>> GetApplicableTeamMembers(Guid applicationId, Guid progressReportId)
    {
        var teamMembers = await _progressReportingProjectTeamRepository
                    .GetProjectTeamMembers(new GetTeamMembersParameters() { ApplicationId = applicationId, ProgressReportId = progressReportId }
                );

        var applicableRoles = new[] { (int)ETeamRole.ProjectManager, (int)ETeamRole.QuantitySurveyor };

        var applicableMembers = new List<GetTeamMembersResult>();
        foreach (var person in teamMembers)
        {
            if (applicableRoles.Contains(person.RoleId ?? 0))
            {
                applicableMembers.Add(person);
            }
        }

        return applicableMembers;
    }
}

public class GetWhoIsTheGrantCertifyingOfficerRequest : IRequest<GetWhoIsTheGrantCertifyingOfficerResponse>
{
    private GetWhoIsTheGrantCertifyingOfficerRequest()
    {
    }

    public static readonly GetWhoIsTheGrantCertifyingOfficerRequest Request = new();
}

public class GetWhoIsTheGrantCertifyingOfficerResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public Guid? ProjectTeamMemberId { get; set; }
    public IList<TeamMemberResponse> TeamMembers { get; set; }

    public class TeamMemberResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
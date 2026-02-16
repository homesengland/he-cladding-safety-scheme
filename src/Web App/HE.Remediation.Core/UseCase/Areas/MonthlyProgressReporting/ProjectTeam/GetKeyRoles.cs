using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam;

public class GetKeyRolesHandler : IRequestHandler<GetKeyRolesRequest, GetKeyRolesResponse>
{
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _projectTeamRepository;

    public GetKeyRolesHandler(
        IApplicationDetailsProvider applicationDetailsProvider, 
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingProjectTeamRepository projectTeamRepository)
    {
        _applicationDetailsProvider = applicationDetailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _projectTeamRepository = projectTeamRepository;
    }


    public async ValueTask<GetKeyRolesResponse> Handle(GetKeyRolesRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _applicationDetailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        var keyRoles = await _projectTeamRepository.GetProjectTeamKeyRoles(new GetProjectTeamKeyRolesParameters
        {
            ApplicationId = details.ApplicationId,
            ProgressReportId = progressReportId
        });

        var teamMembers = await _projectTeamRepository.GetProjectTeamMembers(new GetTeamMembersParameters
        {
            ApplicationId = details.ApplicationId,
            ProgressReportId = progressReportId
        });

        return new GetKeyRolesResponse
        {
            ApplicationReferenceNumber = details.ApplicationReferenceNumber,
            BuildingName = details.BuildingName,
            ApplicationLeadTeamMemberId = keyRoles?.ApplicationLeadTeamMemberId,
            LeaseholderCommunicatorTeamMemberId = keyRoles?.LeaseholderCommunicatorTeamMemberId,
            RegulatoryComplianceTeamMemberId = keyRoles?.RegulatoryComplianceTeamMemberId,
            AllTeamMembers = teamMembers.Select(x => new GetKeyRolesResponse.KeyRolesTeamMemberResponse
            {
                Id = x.Id,
                Name = x.Name,
                RoleName = x.RoleName == "Other"
                    ? (!string.IsNullOrWhiteSpace(x.OtherRole) ? x.OtherRole : "--")
                    : x.RoleName
            }).ToList()
        };
    }
}

public class GetKeyRolesRequest : IRequest<GetKeyRolesResponse>
{
    private GetKeyRolesRequest()
    {
    }

    public static readonly GetKeyRolesRequest Request = new();
}

public class GetKeyRolesResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public Guid? ApplicationLeadTeamMemberId { get; set; }
    public Guid? LeaseholderCommunicatorTeamMemberId { get; set; }
    public Guid? RegulatoryComplianceTeamMemberId { get; set; }

    public IList<KeyRolesTeamMemberResponse> AllTeamMembers { get; set; } = new List<KeyRolesTeamMemberResponse>();

    public class KeyRolesTeamMemberResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
    }
}
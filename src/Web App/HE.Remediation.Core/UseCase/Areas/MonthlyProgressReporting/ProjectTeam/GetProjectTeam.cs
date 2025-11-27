using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;
using System.Linq;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam;

public class GetProjectTeamHandler : IRequestHandler<GetProjectTeamRequest, GetProjectTeamResponse>
{
    private readonly IApplicationDetailsProvider _applicationDetailsProvider;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _projectTeamRepository;

    public GetProjectTeamHandler(
        IApplicationDetailsProvider applicationDetailsProvider, 
        IApplicationDataProvider applicationDataProvider, 
        IProgressReportingProjectTeamRepository projectTeamRepository)
    {
        _applicationDetailsProvider = applicationDetailsProvider;
        _applicationDataProvider = applicationDataProvider;
        _projectTeamRepository = projectTeamRepository;
    }


    public async Task<GetProjectTeamResponse> Handle(GetProjectTeamRequest request, CancellationToken cancellationToken)
    {
        var applicationDetails = await _applicationDetailsProvider.GetApplicationDetails();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var projectTeam = await _projectTeamRepository.GetProjectTeam(new GetProjectTeamParameters
        {
            ApplicationId = applicationDetails.ApplicationId,
            ProgressReportId = progressReportId
        });

        var gco = await _projectTeamRepository.GetGrantCertifyingOfficer(progressReportId);

        var hasAvailableRoles = HasAvailableRoles(projectTeam);

        return new GetProjectTeamResponse
        {
            BuildingName = applicationDetails.BuildingName,
            ApplicationReferenceNumber = applicationDetails.ApplicationReferenceNumber,
            ApplicationLeadTeamMemberId = projectTeam.ApplicationLeadTeamMemberId,
            ApplicationLeadTeamMember = projectTeam.ApplicationLeadTeamMember,
            LeaseholderCommunicatorTeamMemberId = projectTeam.LeaseholderCommunicatorTeamMemberId,
            LeaseholderCommunicatorTeamMember = projectTeam.LeaseholderCommunicatorTeamMember,
            RegulatoryComplianceTeamMemberId = projectTeam.RegulatoryComplianceTeamMemberId,
            RegulatoryComplianceTeamMember = projectTeam.RegulatoryComplicanceTeamMember,
            GrantCertifyingOfficerTeamMemberId = projectTeam.GrantCertifyingOfficerTeamMemberId,
            GrantCertifyingOfficerTeamMember = projectTeam.GrantCertifyingOfficerTeamMember,
            HasAssignedGco = gco?.HasGco,
            HasAvailableRoles = hasAvailableRoles,
            TeamMembers = projectTeam.TeamMembers.Select(x => new GetProjectTeamResponse.ProjectTeamMemberResponse
            {
                Id = x.Id,
                Name = x.Name,
                CompanyName = x.CompanyName,
                Role = x.Role,
                RoleId = x.RoleId
            }).ToList()
        };
    }

    private static bool HasAvailableRoles(Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam.GetProjectTeamResult projectTeam)
    {
        var allOptions = Enum.GetValues<ETeamRole>().ToList();
        var teamMembers = projectTeam.TeamMembers;
        var consumedOptions = teamMembers.Select(tm => (ETeamRole)tm.RoleId);
        var availableOptions = allOptions
            .Except(consumedOptions)
            .Except([ETeamRole.Other]);
        return availableOptions.Any();
    }
}

public class GetProjectTeamRequest : IRequest<GetProjectTeamResponse>
{
    private GetProjectTeamRequest()
    {
    }

    public static readonly GetProjectTeamRequest Request = new();
}

public class GetProjectTeamResponse
{
    public string BuildingName { get; set; }
    public string ApplicationReferenceNumber { get; set; }

    public Guid? ApplicationLeadTeamMemberId { get; set; }
    public string ApplicationLeadTeamMember { get; set; }
    public Guid? LeaseholderCommunicatorTeamMemberId { get; set; }
    public string LeaseholderCommunicatorTeamMember { get; set; }
    public Guid? RegulatoryComplianceTeamMemberId { get; set; }
    public string RegulatoryComplianceTeamMember { get; set; }
    public Guid? GrantCertifyingOfficerTeamMemberId { get; set; }
    public string GrantCertifyingOfficerTeamMember { get; set; }
    public bool? HasAssignedGco { get; set; }

    public IList<ProjectTeamMemberResponse> TeamMembers { get; set; } = new List<ProjectTeamMemberResponse>();
    public bool HasAvailableRoles { get; set; }

    public class ProjectTeamMemberResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
    }
}
using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class TeamMemberViewModelMapper : Profile
{
    public TeamMemberViewModelMapper()
    {
        CreateMap<GetProjectTeamTeamMemberResponse, TeamMemberViewModel>();
        CreateMap<TeamMemberViewModel, UpdateProjectTeamTeamMemberRequest>();
    }
}

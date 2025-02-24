using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetTeamMember;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.UpdateTeamMember;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class TeamMemberViewModelMapper : Profile
{
    public TeamMemberViewModelMapper()
    {
        CreateMap<GetTeamMemberResponse, TeamMemberViewModel>();
        CreateMap<TeamMemberViewModel, UpdateTeamMemberRequest>();
    }
}
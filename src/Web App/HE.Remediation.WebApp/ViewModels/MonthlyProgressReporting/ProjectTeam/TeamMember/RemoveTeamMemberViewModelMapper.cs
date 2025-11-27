using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class RemoveTeamMemberViewModelMapper : Profile
{
    public RemoveTeamMemberViewModelMapper()
    {
        CreateMap<GetRemoveTeamMemberResponse, RemoveTeamMemberViewModel>();
        CreateMap<RemoveTeamMemberViewModel, DeleteTeamMemberRequest>();
    }
}

using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.TeamMember.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.TeamMember.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class TeamMemberViewModelMapper : Profile
{
    public TeamMemberViewModelMapper()
    {
        CreateMap<GetTeamMemberResponse, TeamMemberViewModel>();
        CreateMap<TeamMemberViewModel, UpdateTeamMemberRequest>();
    }
}
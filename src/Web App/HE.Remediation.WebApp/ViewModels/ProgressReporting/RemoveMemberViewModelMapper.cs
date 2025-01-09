using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.DeleteTeamMember;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetRemoveTeamMember;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class RemoveMemberViewModelMapper : Profile
{
    public RemoveMemberViewModelMapper()
    {
        CreateMap<GetRemoveTeamMemberResponse, RemoveMemberViewModel>();
        CreateMap<RemoveMemberViewModel, DeleteTeamMemberRequest>();
    }
}

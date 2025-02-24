using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.Remove.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.Remove.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class RemoveMemberViewModelMapper : Profile
{
    public RemoveMemberViewModelMapper()
    {
        CreateMap<GetRemoveTeamMemberResponse, RemoveMemberViewModel>();
        CreateMap<RemoveMemberViewModel, DeleteTeamMemberRequest>();
    }
}

using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.AddRole.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.AddRole.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class AddViewModelMapper : Profile
{
    public AddViewModelMapper()
    {
        CreateMap<GetAddRoleResponse, AddViewModel>();
        CreateMap<AddViewModel, SetAddRoleRequest>();
    }
}
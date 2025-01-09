using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.GetAddRole;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.SetAddRole;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AddRoleViewModelMapper : Profile
{
    public AddRoleViewModelMapper()
    {
        CreateMap<GetAddRoleResponse, AddRoleViewModel>();
        CreateMap<AddRoleViewModel, SetAddRoleRequest>();
    }
}

using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.AddTeamRole;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.AddTeamRole;
public class AddTeamRoleViewModelMapper : Profile
{
    public AddTeamRoleViewModelMapper()
    {
        CreateMap<GetAddTeamRoleResponse, AddTeamRoleViewModel>();
        CreateMap<AddTeamRoleViewModel, SetAddTeamRoleRequest>();
    }
}

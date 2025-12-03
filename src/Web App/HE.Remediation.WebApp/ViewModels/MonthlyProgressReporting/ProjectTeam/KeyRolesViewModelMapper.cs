using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam;

public class KeyRolesViewModelMapper : Profile
{
    public KeyRolesViewModelMapper()
    {
        CreateMap<GetKeyRolesResponse, KeyRolesViewModel>();
        CreateMap<GetKeyRolesResponse.KeyRolesTeamMemberResponse, KeyRolesViewModel.KeyRolesTeamMemberModel>();

        CreateMap<KeyRolesViewModel, SetKeyRolesRequest>();
    }
}
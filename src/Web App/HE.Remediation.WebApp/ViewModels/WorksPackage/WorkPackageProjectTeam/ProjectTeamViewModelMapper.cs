using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.ProjectTeam.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class ProjectTeamViewModelMapper : Profile
{
    public ProjectTeamViewModelMapper ()
    {
        CreateMap<GetProjectTeamResponse, ProjectTeamViewModel>();
    }
}

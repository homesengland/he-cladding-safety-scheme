using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectTeam.GetProjectTeam;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ProjectTeamViewModelMapper : Profile
{
    public ProjectTeamViewModelMapper ()
    {
        CreateMap<GetProjectTeamResponse, ProjectTeamViewModel>();
    }
}

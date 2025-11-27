using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.NoTeam;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.NoTeam;
public class ProjectTeamNoTeamViewModelMapper : Profile
{
    public ProjectTeamNoTeamViewModelMapper()
    {
        CreateMap<GetProjectTeamNoTeamResponse, ProjectTeamNoTeamViewModel>();
        CreateMap<ProjectTeamNoTeamViewModel, SetProjectTeamNoTeamRequest>();
    }
}

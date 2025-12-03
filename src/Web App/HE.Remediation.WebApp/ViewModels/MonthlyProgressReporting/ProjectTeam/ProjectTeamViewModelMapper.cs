using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam
{
    public class ProjectTeamViewModelMapper : Profile
    {
        public ProjectTeamViewModelMapper()
        {
            CreateMap<GetProjectTeamResponse, ProjectTeamViewModel>();
            CreateMap<GetProjectTeamResponse.ProjectTeamMemberResponse, ProjectTeamViewModel.ProjectTeamMemberViewModel>();
        }
    }
}

using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan
{
    public class ProjectPlanViewModelMapper : Profile
    {
        public ProjectPlanViewModelMapper()
        {
            CreateMap<GetProjectPlanResponse, ProjectPlanViewModel>();
            CreateMap<ProjectPlanViewModel, SetProjectPlanRequest>();
        }
    }
}

using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectSupport
{
    public class ProjectSupportViewModelMapper : Profile
    {
        public ProjectSupportViewModelMapper()
        {
            CreateMap<GetProjectSupportResponse, ProjectSupportViewModel>();
            CreateMap<ProjectSupportViewModel, SetProjectSupportRequest>();
        }
    }
}
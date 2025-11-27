using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectSupport;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectSupport;
public class ProjectSupportCheckYourAnswersViewModelMapper : Profile
{
    public ProjectSupportCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetProjectSupportCheckYourAnswersResponse, ProjectSupportCheckYourAnswersViewModel>();
    }
}

using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan.CheckYourAnswers
{
    public class MonthlyProgressReportProjectPlanCheckYourAnswersViewModelMapper : Profile
    {
        public MonthlyProgressReportProjectPlanCheckYourAnswersViewModelMapper()
        {
            CreateMap<GetMonthlyProgressReportProjectPlanCheckYourAnswersResponse, MonthlyProgressReportProjectPlanCheckYourAnswersViewModel>();
        }
    }
}

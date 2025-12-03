using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.MonthlyProgressReportingProjectPlan;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan.Upload
{
    public class UploadProjectPlanViewModelMapper : Profile
    {
        public UploadProjectPlanViewModelMapper()
        {
            CreateMap<GetUploadProjectPlanResponse, UploadProjectPlanViewModel>();
            CreateMap<UploadProjectPlanViewModel, SetUploadProjectPlanRequest>();
        }
    }
}

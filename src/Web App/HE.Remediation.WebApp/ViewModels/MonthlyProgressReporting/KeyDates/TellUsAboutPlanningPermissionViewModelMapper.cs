using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;
public class TellUsAboutPlanningPermissionViewModelMapper : Profile
{
    public TellUsAboutPlanningPermissionViewModelMapper()
    {
        CreateMap<GetTellUsAboutPlanningPermissionResponse, TellUsAboutPlanningPermissionViewModel>();
        CreateMap<TellUsAboutPlanningPermissionViewModel, SetTellUsAboutPlanningPermissionRequest>();
    }
}

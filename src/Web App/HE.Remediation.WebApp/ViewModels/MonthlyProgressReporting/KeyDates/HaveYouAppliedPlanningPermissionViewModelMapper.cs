using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;
using HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class HaveYouAppliedPlanningPermissionViewModelMapper : Profile
{
    public HaveYouAppliedPlanningPermissionViewModelMapper()
    {
        CreateMap<GetHaveYouAppliedPlanningPermissionResponse, HaveYouAppliedPlanningPermissionViewModel>();
        CreateMap<HaveYouAppliedPlanningPermissionViewModel, SetHaveYouAppliedPlanningPermissionRequest>();
    }
}

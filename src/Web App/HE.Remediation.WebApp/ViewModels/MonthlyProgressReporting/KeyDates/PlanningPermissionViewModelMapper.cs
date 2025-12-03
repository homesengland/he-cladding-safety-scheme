using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class PlanningPermissionViewModelMapper : Profile
{
    public PlanningPermissionViewModelMapper()
    {
        CreateMap<GetPlanningPermissionResponse, PlanningPermissionViewModel>();
        CreateMap<PlanningPermissionViewModel, SetPlanningPermissionRequest>();
    }
}

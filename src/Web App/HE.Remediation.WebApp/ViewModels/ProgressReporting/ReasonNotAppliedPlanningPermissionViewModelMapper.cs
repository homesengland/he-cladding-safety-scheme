using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;
public class ReasonNotAppliedPlanningPermissionViewModelMapper : Profile
{
    public ReasonNotAppliedPlanningPermissionViewModelMapper()
    {
        CreateMap<GetReasonNotAppliedPlanningPermissionResponse, ReasonNotAppliedPlanningPermissionViewModel>();
    }
}

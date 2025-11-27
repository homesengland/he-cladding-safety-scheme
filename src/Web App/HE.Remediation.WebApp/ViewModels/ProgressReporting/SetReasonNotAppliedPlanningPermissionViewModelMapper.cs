using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;
public class SetReasonNotAppliedPlanningPermissionViewModelMapper : Profile
{
    public SetReasonNotAppliedPlanningPermissionViewModelMapper()
    {
        CreateMap<ReasonNotAppliedPlanningPermissionViewModel, SetReasonNotAppliedPlanningPermissionRequest>();
    }
}

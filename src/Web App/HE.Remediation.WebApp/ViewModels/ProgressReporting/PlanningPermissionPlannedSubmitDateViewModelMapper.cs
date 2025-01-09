using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionPlannedSubmitDate.GetPlanningPermissionPlannedSubmitDate;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionPlannedSubmitDate.SetPlanningPermissionPlannedSubmitDate;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class PlanningPermissionPlannedSubmitDateViewModelMapper : Profile
{
    public PlanningPermissionPlannedSubmitDateViewModelMapper()
    {
        CreateMap<GetPlanningPermissionPlannedSubmitDateResponse, PlanningPermissionPlannedSubmitDateViewModel>();
        CreateMap<PlanningPermissionPlannedSubmitDateViewModel, SetPlanningPermissionPlannedSubmitDateRequest>();
    }
}

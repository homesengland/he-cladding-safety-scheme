using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.GetAppliedForPlanningPermission;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.SetAppliedForPlanningPermission;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AppliedPlanningViewModelMapper : Profile
{
    public AppliedPlanningViewModelMapper()
    {
        CreateMap<GetAppliedForPlanningPermissionResponse, AppliedPlanningViewModel>();
        CreateMap<AppliedPlanningViewModel, SetAppliedForPlanningPermissionRequest>();
    }
}

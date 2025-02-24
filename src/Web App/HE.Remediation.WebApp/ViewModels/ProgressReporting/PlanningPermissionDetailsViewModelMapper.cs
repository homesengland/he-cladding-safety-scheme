using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionDetails.GetPlanningPermissionDetails;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.PlanningPermissionDetails.SetPlanningPermissionDetails;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class PlanningPermissionDetailsViewModelMapper : Profile
{
    public PlanningPermissionDetailsViewModelMapper()
    {
        CreateMap<GetPlanningPermissionDetailsResponse, PlanningPermissionDetailsViewModel>();
        CreateMap<PlanningPermissionDetailsViewModel, SetPlanningPermissionDetailsRequest>();
    }
}

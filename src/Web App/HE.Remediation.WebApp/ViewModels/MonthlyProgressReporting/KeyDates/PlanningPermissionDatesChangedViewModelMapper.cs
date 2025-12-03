using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.PlanningPermission;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class PlanningPermissionDatesChangedViewModelMapper : Profile
{
    public PlanningPermissionDatesChangedViewModelMapper()
    {
        CreateMap<GetPlanningPermissionDatesChangedResponse, PlanningPermissionDatesChangedViewModel>();
        CreateMap<PlanningPermissionDatesChangedViewModel, SetPlanningPermissionDatesChangedRequest>();
    }
}
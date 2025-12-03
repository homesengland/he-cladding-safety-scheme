using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class WorksPlanningDatesChangedViewModelMapper : Profile
{
    public WorksPlanningDatesChangedViewModelMapper()
    {
        CreateMap<GetWorksPlanningDatesChangedResponse, WorksPlanningDatesChangedViewModel>();
        CreateMap<WorksPlanningDatesChangedViewModel, SetWorksPlanningDatesChangedRequest>();
    }
}
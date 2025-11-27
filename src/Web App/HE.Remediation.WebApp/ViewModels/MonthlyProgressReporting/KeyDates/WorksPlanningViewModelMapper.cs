using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class WorksPlanningViewModelMapper : Profile
{
    public WorksPlanningViewModelMapper()
    {
        CreateMap<GetWorksPlanningResponse, WorksPlanningViewModel>();
        CreateMap<WorksPlanningViewModel, SetWorksPlanningRequest>();
    }
}
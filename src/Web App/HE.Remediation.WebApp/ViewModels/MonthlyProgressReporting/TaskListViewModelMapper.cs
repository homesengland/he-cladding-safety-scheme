using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting;

public class TaskListViewModelMapper : Profile
{
    public TaskListViewModelMapper()
    {
        CreateMap<GetTaskListResponse, TaskListViewModel>();
    }
}

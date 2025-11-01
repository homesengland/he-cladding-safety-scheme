using AutoMapper;

using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.TaskList.GetTaskList;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class TaskListViewModelMapper : Profile
{
    public TaskListViewModelMapper()
    {
        CreateMap<GetTaskListResponse, TaskListViewModel>();
    }
}
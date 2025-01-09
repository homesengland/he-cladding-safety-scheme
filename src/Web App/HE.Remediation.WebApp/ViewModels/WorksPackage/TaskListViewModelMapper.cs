using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.TaskList.GetTaskList;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage;

public class TaskListViewModelMapper : Profile
{
    public TaskListViewModelMapper()
    {
        CreateMap<GetTaskListResponse, TaskListViewModel>();
    }
}

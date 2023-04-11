using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.TaskList.GetTaskList;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class TaskListViewModelMapper :Profile
    {
        public TaskListViewModelMapper()
        {
            CreateMap<GetTaskListResponse, TaskListViewModel>();
        }
    }
}

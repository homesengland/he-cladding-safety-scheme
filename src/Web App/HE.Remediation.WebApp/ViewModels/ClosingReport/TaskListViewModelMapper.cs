using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.TaskList.GetTaskList;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class TaskListViewModelMapper : Profile
{
    public TaskListViewModelMapper()
    {
        CreateMap<GetTaskListResponse, TaskListViewModel>();

        CreateMap<
            GetTaskListResponse.TaskWithStatus,
            TaskListViewModel.TaskWithStatus
        >();
    }
}

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.TaskList.GetTaskList
{
    public class GetTaskListRequest : IRequest<GetTaskListResponse>
    {
        private GetTaskListRequest()
        {
        }

        public static GetTaskListRequest Request => new();
    }
}
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.TaskList.GetTaskList
{
    public class GetTaskListRequest : IRequest<GetTaskListResponse>
    {
        internal GetTaskListRequest()
        {
                
        }

        public static GetTaskListRequest Request => new();
    }
}

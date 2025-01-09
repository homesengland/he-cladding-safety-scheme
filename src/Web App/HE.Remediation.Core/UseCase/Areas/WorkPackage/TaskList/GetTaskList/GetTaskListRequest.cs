using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.TaskList.GetTaskList;

public class GetTaskListRequest : IRequest<GetTaskListResponse>
{
    private GetTaskListRequest()
    {
    }

    public static GetTaskListRequest Request => new();
}

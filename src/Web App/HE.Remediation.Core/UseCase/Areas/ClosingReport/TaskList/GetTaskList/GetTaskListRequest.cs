using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.TaskList.GetTaskList;

public class GetTaskListRequest : IRequest<GetTaskListResponse>
{
    private GetTaskListRequest()
    {
    }

    public static GetTaskListRequest Request => new();
}

using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Get;

public class GetProjectDatesRequest : IRequest<GetProjectDatesResponse>
{
    private GetProjectDatesRequest()
    {
    }

    public static GetProjectDatesRequest Request => new();
}

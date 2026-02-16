using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ConfirmChangeProjectDates.Get;

public class GetConfirmChangeProjectDatesRequest : IRequest<GetConfirmChangeProjectDatesResponse>
{
    private GetConfirmChangeProjectDatesRequest()
    {
    }

    public static GetConfirmChangeProjectDatesRequest Request => new();
}

using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport;
public class GetProgressSupportTypeRequest : IRequest<GetProgressSupportTypeResponse>
{
    private GetProgressSupportTypeRequest()
    {
    }

    public static readonly GetProgressSupportTypeRequest Request = new();
}

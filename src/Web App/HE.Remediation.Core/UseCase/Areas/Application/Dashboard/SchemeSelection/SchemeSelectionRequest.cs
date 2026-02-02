using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.Dashboard.SchemeSelection
{
    public class SchemeSelectionRequest : IRequest<SchemeSelectionResponse>
    {
        public static readonly SchemeSelectionRequest Request = new();
    }
}

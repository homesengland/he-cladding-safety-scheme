using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectSupport
{
    public class GetProjectSupportRequest : IRequest<GetProjectSupportResponse>
    {
        public Guid ProgressReportId { get; set; }
    }
}

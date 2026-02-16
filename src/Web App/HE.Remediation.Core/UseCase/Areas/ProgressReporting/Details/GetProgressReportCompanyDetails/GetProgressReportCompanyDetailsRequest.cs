using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportCompanyDetails;

public class GetProgressReportCompanyDetailsRequest : IRequest<GetProgressReportCompanyDetailsResponse>
{
    public Guid TeamMemberId { get; set; }
}
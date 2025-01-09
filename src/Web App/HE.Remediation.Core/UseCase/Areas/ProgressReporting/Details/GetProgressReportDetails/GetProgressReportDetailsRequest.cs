using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.Details.GetProgressReportDetails;

public class GetProgressReportDetailsRequest : IRequest<GetProgressReportDetailsResponse>
{
    public Guid ProgressReportId { get; set; }
}
using Mediator;
namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetForecastGateway3Submission;

public class SetForecastGateway3SubmissionRequest : IRequest
{
    public int? ExpectedSubmissionDateMonth { get; set; }
    public int? ExpectedSubmissionDateYear { get; set; }
}

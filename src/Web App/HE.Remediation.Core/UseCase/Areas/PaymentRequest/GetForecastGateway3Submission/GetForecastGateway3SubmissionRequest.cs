using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetForecastGateway3Submission;

public class GetForecastGateway3SubmissionRequest : IRequest<GetForecastGateway3SubmissionResponse>
{
    private GetForecastGateway3SubmissionRequest()
    {
    }

    public static readonly GetForecastGateway3SubmissionRequest Request = new();
}


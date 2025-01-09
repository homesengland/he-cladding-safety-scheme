using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.CostReport.GetCostReport;

public class GetCostRequest : IRequest<GetCostResponse>
{
    private GetCostRequest()
    {
    }

    public static readonly GetCostRequest Request = new();
}

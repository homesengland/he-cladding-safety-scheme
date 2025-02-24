using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetUploadCostReport;

public class GetUploadCostReportRequest : IRequest<GetUploadCostReportResponse>
{
    private GetUploadCostReportRequest()
    {
    }

    public static readonly GetUploadCostReportRequest Request = new();
}

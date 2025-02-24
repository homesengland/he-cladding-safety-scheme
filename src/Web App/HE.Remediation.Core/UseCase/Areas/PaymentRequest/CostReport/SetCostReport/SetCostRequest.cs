using MediatR;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.CostReport.SetCostReport;

public class SetCostRequest : IRequest
{
    public IFormFile File { get; set; }
}

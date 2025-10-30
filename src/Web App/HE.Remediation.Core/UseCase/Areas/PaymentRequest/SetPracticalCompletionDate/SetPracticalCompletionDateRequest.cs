using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetPracticalCompletionDate;

public class SetPracticalCompletionDateRequest : IRequest
{
    public int? ExpectedPracticalDateMonth { get; set; }
    public int? ExpectedPracticalDateYear { get; set; }
}

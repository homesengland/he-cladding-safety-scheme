using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetConfirmation;

public class SetConfirmationRequest : IRequest
{
    public bool? FinalCostReport { get; set; }
    public bool? ExitFraew { get; set; }
    public bool? CompletionCertificate { get; set; }
    public bool? InformedPracticalCompletion { get; set; }
}

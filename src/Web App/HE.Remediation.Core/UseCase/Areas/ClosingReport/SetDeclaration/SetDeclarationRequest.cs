using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetDeclaration;

public class SetDeclarationRequest : IRequest
{
    public DateTime? DateOfCompletion { get; set; }

    public bool? FraewRiskToLifeReduced { get; set; }
}
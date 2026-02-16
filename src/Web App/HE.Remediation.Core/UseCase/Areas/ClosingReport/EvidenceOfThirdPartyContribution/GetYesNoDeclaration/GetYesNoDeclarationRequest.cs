using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.GetYesNoDeclaration;

public class GetYesNoDeclarationRequest : IRequest<GetYesNoDeclarationResponse>
{
    private GetYesNoDeclarationRequest()
    {
    }

    public static readonly GetYesNoDeclarationRequest Request = new();
}

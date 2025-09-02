using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.GetYesNoDeclaration;

public class GetYesNoDeclarationResponse
{
    public ENoYes? Declaration { get; set; }
    public bool IsSubmitted { get; set; }
}
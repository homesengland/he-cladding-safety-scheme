using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.PreferredContractorLinks.Set;

public class SetPreferredContractorLinksRequest : IRequest
{
    public EYesNoNonBoolean? PreferredContractorLinks { get; set; }
    public string PreferredContractorLinkAdditionalNotes { get; set; }
}

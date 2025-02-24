
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDeclaration.Declaration.Set;

public class SetDeclarationRequest : IRequest
{
    public bool? AllCostsReasonable { get; set; }
    
    public bool? AllContractualRequirementsMet { get; set; }

    public bool? AllCostsSetOutInFull { get; set; }

    public bool? AcceptGrantAwardBasedOnCosts { get; set; }
}

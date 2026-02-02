using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Set;

public class SetRequiresSubcontractorsRequest : IRequest
{
    public ENoYes? RequiresSubcontractors { get; set; }
}

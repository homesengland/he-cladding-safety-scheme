using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Set;

public class SetRequiresSubcontractorsRequest : IRequest
{
    public ENoYes? RequiresSubcontractors { get; set; }
}

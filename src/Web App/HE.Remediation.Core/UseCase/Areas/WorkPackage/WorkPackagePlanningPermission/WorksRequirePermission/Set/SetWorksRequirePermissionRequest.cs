
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.WorksRequirePermission.Set;

public class SetWorksRequirePermissionRequest : IRequest
{
    public bool? PermissionRequired { get; set; }

    public string ReasonPermissionNotRequired { get; set; }
}

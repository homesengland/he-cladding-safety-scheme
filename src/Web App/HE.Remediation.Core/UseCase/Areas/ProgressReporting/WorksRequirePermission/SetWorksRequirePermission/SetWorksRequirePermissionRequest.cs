
using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.SetWorksRequirePermission;

public class SetWorksRequirePermissionRequest : IRequest
{
    public EYesNoNonBoolean? PermissionRequired { get; set; }
}

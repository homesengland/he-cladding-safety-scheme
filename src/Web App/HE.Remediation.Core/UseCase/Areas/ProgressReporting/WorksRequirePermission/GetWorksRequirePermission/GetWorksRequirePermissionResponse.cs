
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.GetWorksRequirePermission;

public class GetWorksRequirePermissionResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public EYesNoNonBoolean? PermissionRequired { get; set; }
    public bool? QuotesSought { get; set; }
    public int Version { get; set; }
}

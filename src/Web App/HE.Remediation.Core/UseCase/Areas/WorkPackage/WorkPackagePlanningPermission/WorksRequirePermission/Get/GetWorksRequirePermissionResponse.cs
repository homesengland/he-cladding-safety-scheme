
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.WorksRequirePermission.Get;

public class GetWorksRequirePermissionResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool? PermissionRequired { get; set; }

    public string ReasonPermissionNotRequired { get; set; }

    public bool IsSubmitted { get; set; }
}

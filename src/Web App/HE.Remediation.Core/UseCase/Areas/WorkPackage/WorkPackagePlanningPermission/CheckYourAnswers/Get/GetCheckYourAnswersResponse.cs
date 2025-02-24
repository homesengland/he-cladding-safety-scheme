
namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.CheckYourAnswers.Get;

public class GetCheckYourAnswersResponse
{
    public bool? PermissionRequired { get; set; }

    public string ReasonPermissionNotRequired { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; internal set; }
}

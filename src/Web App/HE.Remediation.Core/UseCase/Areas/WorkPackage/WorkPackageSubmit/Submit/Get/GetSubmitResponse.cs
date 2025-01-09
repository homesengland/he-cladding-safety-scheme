namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSubmit.Submit.Get;

public class GetSubmitResponse
{
    public bool IsSubmitted { get; set; }

    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }
}

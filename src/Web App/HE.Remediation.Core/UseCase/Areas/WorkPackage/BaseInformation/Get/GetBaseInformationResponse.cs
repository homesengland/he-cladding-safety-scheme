namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;

public class GetBaseInformationResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }
}

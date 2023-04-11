namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmBuildingHeight.GetBuildingHeight;

public class GetBuildingHeightResponse
{
    public int? NumberOfStoreys { get; set; }
    public DateTime? CorrectHeightConfirmedDate { get; set; }
}
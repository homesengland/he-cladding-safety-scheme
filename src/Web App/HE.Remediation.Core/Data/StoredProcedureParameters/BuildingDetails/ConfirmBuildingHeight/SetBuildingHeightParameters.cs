namespace HE.Remediation.Core.Data.StoredProcedureParameters.BuildingDetails.ConfirmBuildingHeight;
public class SetBuildingHeightParameters
{
    public Guid ApplicationId { get; set; }
    public int? NumberOfStoreys { get; set; }
    public decimal? BuildingHeight { get; set; }
}

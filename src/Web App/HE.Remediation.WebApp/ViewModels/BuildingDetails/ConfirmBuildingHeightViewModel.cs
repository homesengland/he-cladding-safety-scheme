using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ConfirmBuildingHeightViewModel
{
    public int? NumberOfStoreys { get; set; }
    public decimal? BuildingHeight { get; set; }
    public DateTime? CorrectHeightConfirmedDate { get; set; }
    public ESubmitAction SubmitAction { get; set; }
    public string ReturnUrl { get; set; }
}
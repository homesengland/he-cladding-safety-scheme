using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.GetBuildingDeveloperInformation;

public class GetBuildingDeveloperInformationResponse
{
    public bool? BuildingHasSafetyRegulatorRegistrationCode { get; set; }
    public bool? DoYouKnowOriginalDeveloper { get; set; }    
    public EApplicationScheme ApplicationScheme { get; set; }
}
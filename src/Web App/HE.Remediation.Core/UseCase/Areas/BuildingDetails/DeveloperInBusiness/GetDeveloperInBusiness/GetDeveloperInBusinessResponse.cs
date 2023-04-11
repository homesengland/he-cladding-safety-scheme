using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.GetDeveloperInBusiness;

public class GetDeveloperInBusinessResponse
{
    public EApplicationDeveloperInBusinessType? IsOriginalDeveloperStillInBusiness { get; set; }
}
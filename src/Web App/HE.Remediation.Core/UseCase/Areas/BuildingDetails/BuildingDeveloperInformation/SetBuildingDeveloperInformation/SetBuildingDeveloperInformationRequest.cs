using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperInformation;

public class SetBuildingDeveloperInformationRequest : IRequest
{
    public bool? DoYouKnowOriginalDeveloper { get; set; }    
}
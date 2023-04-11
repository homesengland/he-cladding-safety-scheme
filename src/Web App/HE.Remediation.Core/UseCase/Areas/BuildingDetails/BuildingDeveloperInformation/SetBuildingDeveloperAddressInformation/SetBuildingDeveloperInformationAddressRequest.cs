using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingDeveloperInformation.SetBuildingDeveloperAddressInformation;

public class SetBuildingDeveloperInformationAddressRequest : IRequest
{
    public string OrganisationName { get; set; }
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string? County { get; set; }
    public string Postcode { get; set; }
}

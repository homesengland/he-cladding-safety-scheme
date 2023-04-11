using HE.Remediation.Core.Services.Location;
using System.Text.Json;

namespace HE.Remediation.Core.UseCase.Areas.Location.PostCode;

public static class PostCodeUtility
{
    /// <summary>
    /// Splits a comma separated string into its separate parts (house number). This relies on
    /// strings generated for output instead of what we are provided by HE 
    /// </summary>
    /// <param name="address">a string containing a comma separated address</param>
    /// <returns></returns>
    public static ParsedAddress ParseAddressJson(string addressJson)
    {
        var address = JsonSerializer.Deserialize<PostCodeResult>(addressJson);

        return new ParsedAddress
        {
            NameNumber = address.BuildingNumber ?? address.SubBuildingName ?? address.Organisation,
            AddressLine1 = address.BuildingName ?? address.Street,
            AddressLine2 = address.BuildingNumber is null ? address.Street: string.Empty,
            City = address.Locality ?? address.Town,
            County = address.Locality is null ? string.Empty : address.Town,
            Postcode = address.Postcode
        };
    }
}

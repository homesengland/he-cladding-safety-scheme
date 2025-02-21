using HE.Remediation.Core.Services.Location;
using System.Text.Json;

namespace HE.Remediation.Core.UseCase.Areas.Location.PostCode;

public static class PostCodeUtility
{
    /// <summary>
    /// Splits a comma separated string into its separate parts (house number). This relies on
    /// strings generated for output instead of what we are provided by HE 
    /// </summary>
    /// <param name="addressJson">a string containing a comma separated address</param>
    /// <returns></returns>
    public static ParsedAddress ParseAddressJson(string addressJson)
    {
        var address = JsonSerializer.Deserialize<PostCodeResult>(addressJson);

        var parsedXCoordinate = address.XCoordinate != decimal.MinValue ? address.XCoordinate.ToString() : null;
        var parsedYCoordinate = address.YCoordinate != decimal.MinValue ? address.YCoordinate.ToString() : null;
        
        return new ParsedAddress
        {
            NameNumber = address.BuildingNumber ?? address.SubBuildingName ?? address.Organisation,
            AddressLine1 = address.BuildingName ?? address.Street,
            AddressLine2 = address.BuildingName != null && address.BuildingNumber == null ? address.Street: string.Empty,
            City = address.Locality ?? address.Town,
            County = address.Locality is null ? string.Empty : address.Town,
            Postcode = address.Postcode,
            SubBuildingName = address.SubBuildingName,
            BuildingName = address.BuildingName,
            BuildingNumber = address.BuildingNumber,
            Street = address.Street,
            Town = address.Town,
            AdminArea = address.AdminArea,
            UPRN = address.UPRN,
            AddressLines = address.Address,
            XCoordinate = parsedXCoordinate,
            YCoordinate = parsedYCoordinate,
            Toid = address.Toid,
            BuildingType = address.BuildingType,
            LocalAuthority = address.Locality ?? address.AdminArea
        };
    }

    public static ParsedAddress ParseBuildingLookupJson(string json)
    {
        var address = JsonSerializer.Deserialize<PostCodeResult>(json);

        var parsedXCoordinate = address.XCoordinate != decimal.MinValue ? address.XCoordinate.ToString() : null;
        var parsedYCoordinate = address.YCoordinate != decimal.MinValue ? address.YCoordinate.ToString() : null;
        
        return new ParsedAddress
        {
            NameNumber = address?.BuildingName ?? address?.BuildingNumber,
            AddressLine1 = !string.IsNullOrEmpty(address?.BuildingName) && !string.IsNullOrEmpty(address.BuildingNumber) ? $"{address.BuildingNumber} {address.Street}" : address?.Street,
            LocalAuthority = address?.Locality ?? address?.AdminArea,
            City = address?.Town,
            Postcode = address?.Postcode,
            SubBuildingName = address?.SubBuildingName,
            BuildingName = address?.BuildingName,
            BuildingNumber = address?.BuildingNumber,
            Street = address?.Street,
            Town = address?.Town,
            AdminArea = address?.AdminArea,
            UPRN = address?.UPRN,
            AddressLines = address?.Address,
            XCoordinate = parsedXCoordinate,
            YCoordinate = parsedYCoordinate,
            Toid = address?.Toid,
            BuildingType = address?.BuildingType
        };
    }
}
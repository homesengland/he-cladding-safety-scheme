using System.Text.Json;
using System.Text.RegularExpressions;
using FuzzySharp;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Services.Location;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;

namespace HE.Remediation.Core.UseCase.DataIngest.Lookups
{
    public interface IAddressResolver
    {
        Task<BuildingDetailsAddressDetails> GetAddress(ImportedDataRow cssMapper);
    }

    public class AddressResolver : IAddressResolver
    {
        private readonly IPostCodeLookup _postCodeLookup;

        public AddressResolver(IPostCodeLookup postCodeLookup)
        {
            _postCodeLookup = postCodeLookup;
        }

        public async Task<BuildingDetailsAddressDetails> GetAddress(ImportedDataRow importedDataRow)
        {
            var results = await _postCodeLookup.SearchBuildings(importedDataRow.PostCode);

            if (results == null || results.Locations.Count == 0)
            {
                throw new AddressResolverException($"No locations found for the provided postcode ({importedDataRow.PostCode}).");
            }

            var fuzzyMatchResult = FuzzyMatch(importedDataRow, results.Locations);

            if (!fuzzyMatchResult.Item1)
            {
                throw new AddressResolverException($"No location matched the address ({importedDataRow.BuildingName}, {importedDataRow.AddressLine1}).");
            }

            var parsedAddress = PostCodeUtility.ParseBuildingLookupJson(JsonSerializer.Serialize(fuzzyMatchResult.Item2));

            if (parsedAddress == null)
            {
                throw new AddressResolverException("Resolved address not in expected format. Check Postcode API response.");
            }

            var details = new BuildingDetailsAddressDetails
            {
                NameNumber = parsedAddress.NameNumber,
                AddressLine1 = parsedAddress.AddressLine1,
                AddressLine2 = parsedAddress.AddressLine2,
                City = parsedAddress.City,
                LocalAuthority = parsedAddress.LocalAuthority,
                County = parsedAddress.County,
                Postcode = parsedAddress.Postcode,
                SubBuildingName = parsedAddress.SubBuildingName,
                BuildingName = importedDataRow.BuildingName,
                BuildingNumber = parsedAddress.BuildingNumber,
                Street = parsedAddress.Street,
                Town = parsedAddress.Town,
                AdminArea = parsedAddress.AdminArea,
                UPRN = parsedAddress.UPRN,
                AddressLines = parsedAddress.AddressLines,
                XCoordinate = parsedAddress.XCoordinate,
                YCoordinate = parsedAddress.YCoordinate,
                Toid = parsedAddress.Toid,
                BuildingType = parsedAddress.BuildingType
            };

            return details;
        }

        private (bool, PostCodeResult) FuzzyMatch(ImportedDataRow importedDataRow, List<PostCodeResult> locations)
        {
            foreach (var location in locations)
            {
                var score = 0;
                var buildingNameMatch = false;
                var addressLine1Match = false;

                var importedBuildingName = Regex.Replace(importedDataRow.BuildingName ?? "", @"[^a-zA-Z\s]", "").Trim().ToLowerInvariant();
                var importedStreet = Regex.Replace(importedDataRow.AddressLine1 ?? "", @"[^a-zA-Z\s]", "").Trim().ToLowerInvariant();

                var locationBuildingName = location.BuildingName?.ToLowerInvariant() ?? string.Empty;
                var locationStreet = location.Street?.ToLowerInvariant() ?? string.Empty;

                // match on Building Name

                score = Fuzz.Ratio(importedBuildingName, locationBuildingName);
                buildingNameMatch = buildingNameMatch || score > 75;

                score = Fuzz.Ratio(importedBuildingName, locationStreet);
                buildingNameMatch = buildingNameMatch || score > 75;

                buildingNameMatch = buildingNameMatch || importedBuildingName.Contains(locationBuildingName);

                // match on Address Line 1

                score = Fuzz.Ratio(importedStreet, locationBuildingName);
                addressLine1Match = addressLine1Match || score > 75;

                score = Fuzz.Ratio(importedStreet, locationStreet);
                addressLine1Match = addressLine1Match || score > 75;

                addressLine1Match = addressLine1Match || importedStreet.Contains(locationStreet);

                var isAcceptableMatch = buildingNameMatch || addressLine1Match;

                return (isAcceptableMatch, location);
            }

            return (false, null);
        }

        public class AddressLookup
        {
            public Guid AddressId { get; set; }
            public string NameNumber { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string County { get; set; }
            public string Postcode { get; set; }
            public string LocalAuthority { get; set; }
            public string SubBuildingName { get; set; }
            public string BuildingName { get; set; }
            public string BuildingNumber { get; set; }
            public string Street { get; set; }
            public string Town { get; set; }
            public string AdminArea { get; set; }
            public string UPRN { get; set; }
            public string AddressLines { get; set; }
            public string XCoordinate { get; set; }
            public string YCoordinate { get; set; }
            public string Toid { get; set; }
            public string BuildingType { get; set; }
        }

        public class AddressResolverException : DataImportException
        {
            public AddressResolverException(string message) : base($"Postcode Lookup: {message}")
            {
            }
        }
    }
}
using System.Text.Json;
using Moq;
using HE.Remediation.Core.UseCase.DataIngest;
using HE.Remediation.Core.Services.Location;
using HE.Remediation.Core.UseCase.DataIngest.Lookups;

namespace HE.Remediation.Core.Tests.UseCase.DataIngest
{
    public class AddressResolverTests
    {
        private readonly Mock<IPostCodeLookup> _postCodeLookupMock;
        private readonly AddressResolver _resolver;

        public AddressResolverTests()
        {
            _postCodeLookupMock = new Mock<IPostCodeLookup>();
            _resolver = new AddressResolver(_postCodeLookupMock.Object);
        }

        [Fact]
        public async Task GetAddress_ReturnsBuildingDetails_WhenMatchFound()
        {
            // Arrange
            var importedData = new ImportedDataRow(new Dictionary<string, string>
            {
                { "Building_Name", "Colne House (1-100)" },
                { "Address_Line_1", "103 Harts Lane" },
                { "Postcode", "IG11 8LS" }
            });

            var jsonResponse = "[{\"Address\":\"COLNE HOUSE, 103, HARTS LANE, BARKING, BARKING AND DAGENHAM, IG11 8LS\",\"AdminArea\":\"BARKING AND DAGENHAM\",\"BuildingName\":\"COLNE HOUSE\",\"BuildingNumber\":\"103\",\"Department\":null,\"DependentLocality\":null,\"DependentStreet\":null,\"Locality\":null,\"Organisation\":null,\"Postcode\":\"IG11 8LS\",\"Street\":\"HARTS LANE\",\"SubBuildingName\":null,\"SubBuildingNumber\":null,\"Town\":\"BARKING\",\"UPRN\":\"100105203\",\"XCoordinate\":543720,\"YCoordinate\":184527,\"Toid\":\"osgb1000042418606\",\"BuildingType\":\"Property Shell\"}," +
                                "{\"Address\":\"103, HARTS LANE, BARKING, BARKING AND DAGENHAM, IG11 8LS\",\"AdminArea\":\"BARKING AND DAGENHAM\",\"BuildingName\":null,\"BuildingNumber\":\"103\",\"Department\":null,\"DependentLocality\":null,\"DependentStreet\":null,\"Locality\":null,\"Organisation\":null,\"Postcode\":\"IG11 8LS\",\"Street\":\"HARTS LANE\",\"SubBuildingName\":null,\"SubBuildingNumber\":null,\"Town\":\"BARKING\",\"UPRN\":\"100105203\",\"XCoordinate\":543720,\"YCoordinate\":184527,\"Toid\":\"osgb1000042418606\",\"BuildingType\":\"Property Shell\"}," +
                                "{\"Address\":\"COLNE HOUSE, HARTS LANE, BARKING, BARKING AND DAGENHAM, IG11 8LS\",\"AdminArea\":\"BARKING AND DAGENHAM\",\"BuildingName\":\"COLNE HOUSE\",\"BuildingNumber\":null,\"Department\":null,\"DependentLocality\":null,\"DependentStreet\":null,\"Locality\":null,\"Organisation\":null,\"Postcode\":\"IG11 8LS\",\"Street\":\"HARTS LANE\",\"SubBuildingName\":null,\"SubBuildingNumber\":null,\"Town\":\"BARKING\",\"UPRN\":\"100105203\",\"XCoordinate\":543720,\"YCoordinate\":184527,\"Toid\":\"osgb1000042418606\",\"BuildingType\":\"Property Shell\"}]";

            var locations = JsonSerializer.Deserialize<List<PostCodeResult>>(jsonResponse);

            _postCodeLookupMock
                .Setup(x => x.SearchBuildings("IG11 8LS"))
                .ReturnsAsync(new PostCodeResults { Locations = locations });

            // Act
            var result = await _resolver.GetAddress(importedData);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Colne House (1-100)", result.BuildingName, ignoreCase: true);
            Assert.Equal("103 Harts Lane", result.AddressLine1, ignoreCase: true);
            Assert.Equal("IG11 8LS", result.Postcode, ignoreCase: true);
        }

        [Fact]
        public async Task GetAddress_Throws_WhenNoLocationsFound()
        {
            // Arrange
            var importedData = new ImportedDataRow(new Dictionary<string, string>
            {
                { "Address_Line_1", "10 Main Street" },
                { "Postcode", "AB12 3CD" }
            });

            _postCodeLookupMock
                .Setup(x => x.SearchBuildings("AB12 3CD"))
                .ReturnsAsync(new PostCodeResults { Locations = new List<PostCodeResult>() });

            // Act & Assert
            var ex = await Assert.ThrowsAsync<AddressResolver.AddressResolverException>(() => _resolver.GetAddress(importedData));
            Assert.Contains("No locations found", ex.Message);
        }

        [Fact]
        public async Task GetAddress_Throws_WhenNoFuzzyMatch()
        {
            // Arrange
            var importedData = new ImportedDataRow(new Dictionary<string, string>
            {
                { "Address_Line_1", "10 Main Street" },
                { "Postcode", "AB12 3CD" }
            });

            var location = new PostCodeResult
            {
                BuildingName = "Other Building",
                BuildingNumber = "99",
                Street = "Other Street",
                Postcode = "AB12 3CD"
            };

            _postCodeLookupMock
                .Setup(x => x.SearchBuildings("AB12 3CD"))
                .ReturnsAsync(new PostCodeResults { Locations = new List<PostCodeResult> { location } });

            // Act & Assert
            var ex = await Assert.ThrowsAsync<AddressResolver.AddressResolverException>(() => _resolver.GetAddress(importedData));
            Assert.Contains("No location matched", ex.Message);
        }
       
    }
}

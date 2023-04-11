using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.GetReportDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetReportDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetReportDetailsHandler _handler;
        
    public GetReportDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetReportDetailsHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Combined_Data_From_DB()
    {
        //Arrange        
        var buildingDetails = new FRAEWBuildingDetails()
        { 
            BuildingAddress = "123 High Road, Dalston",
            FRAEWInstructedDate = DateTime.Now.AddYears(-1),
            BuildingName = "My building",
            FRAEWCompletedDate = DateTime.Now.AddYears(-1).AddDays(2),
            CompanyUndertakingReport = "Undertakers"
        };

        var reportDetails = new FRAEWReportDetails()
        {
            AuthorsName = "Author",
            PeerReviewPerson = "Peer person",
            UndertakingFirm = "The company",
            NumberOfStoreys = 2,
            BuildingHeight = 12,
            BuildingInterimMeasures = ENoYes.Yes,
            BasicComplexId = EBasicComplexType.Basic                 
        };


        _connection.Setup(x => x.QuerySingleOrDefaultAsync<FRAEWBuildingDetails>("GetFRAEWBuildingDetails", It.IsAny<object>()))
                                .ReturnsAsync(buildingDetails)
                                .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<FRAEWReportDetails>("GetFireRiskAssessmentReportDetails", It.IsAny<object>()))
                                .ReturnsAsync(reportDetails)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); ;

        //// Act
        var result = await _handler.Handle(GetReportDetailsRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.AuthorsName == "Author") &&
                           (result.PeerReviewPerson == "Peer person") &&
                           (result.UndertakingFirm == "The company") &&
                           (result.BuildingAddress == "123 High Road, Dalston"));        
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }

    [Fact]
    public async Task Handler_Returns_Null_From_DB()
    {
        //Arrange        

        var buildingDetails = new FRAEWBuildingDetails();
        var reportDetails = new FRAEWReportDetails();        

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<FRAEWBuildingDetails>("GetFRAEWBuildingDetails", It.IsAny<object>()))
                                .ReturnsAsync(buildingDetails)
                                .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<FRAEWReportDetails>("GetFireRiskAssessmentReportDetails", It.IsAny<object>()))
                                .ReturnsAsync(reportDetails)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        //// Act
        var result = await _handler.Handle(GetReportDetailsRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.AuthorsName == null) &&
                           (result.PeerReviewPerson == null) &&
                           (result.UndertakingFirm == null) &&
                           (result.BuildingAddress == null));        
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}

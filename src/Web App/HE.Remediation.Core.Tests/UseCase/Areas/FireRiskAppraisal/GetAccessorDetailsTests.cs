using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.GetAssessorDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetAccessorDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetAssessorDetailsHandler _handler;
        
    public GetAccessorDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetAssessorDetailsHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        GetAssessorDetailsResponse accessorDetails = new GetAssessorDetailsResponse
        {
            FirstName = "First Name",
            LastName = "Last Name",
            CompanyName = "My company",
            CompanyNumber = "123456",
            EmailAddress = "test@test.com",
            Telephone = "01962 123456"
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetAssessorDetailsResponse>("GetFireRiskAssessmentAssessorDetails", It.IsAny<object>()))
                                .ReturnsAsync(accessorDetails)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetAssessorDetailsRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&                           
                           (result.FirstName == "First Name") &&
                           (result.LastName == "Last Name") &&
                           (result.CompanyName == "My company"));        
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}

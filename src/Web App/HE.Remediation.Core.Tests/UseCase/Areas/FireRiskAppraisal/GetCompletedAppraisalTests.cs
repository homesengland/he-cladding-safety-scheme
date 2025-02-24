using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CompletedAppraisal.GetCompletedAppraisal;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetCompletedAppraisalTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetCompletedAppraisalHandler _handler;
        
    public GetCompletedAppraisalTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetCompletedAppraisalHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        GetCompletedAppraisalResponse accessorDetails = new GetCompletedAppraisalResponse
        {
            IsAppraisalCompleted = true
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetCompletedAppraisalResponse>("GetFireRiskCompletedStatus", It.IsAny<object>()))
                                .ReturnsAsync(accessorDetails)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetCompletedAppraisalRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.IsAppraisalCompleted.Value));        
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }

    [Fact]
    public async Task Handler_Returns_Null_From_DB()
    {
        //Arrange        
        GetCompletedAppraisalResponse accessorDetails = new GetCompletedAppraisalResponse
        {
            IsAppraisalCompleted = null
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetCompletedAppraisalResponse>("GetFireRiskCompletedStatus", It.IsAny<object>()))
                                .ReturnsAsync(accessorDetails)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetCompletedAppraisalRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (!result.IsAppraisalCompleted.HasValue));        
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}

using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetLeaseHolderEvidence;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Leaseholder;

public class GetLeaseHolderEvidenceTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;

    private readonly GetLeaseHolderEvidenceHandler _handler;
        
    public GetLeaseHolderEvidenceTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetLeaseHolderEvidenceHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        Guid firstEvidenceGuid = Guid.NewGuid();
        Guid secondEvidenceGuid = Guid.NewGuid();

        var evidenceResponse = new List<GetLeaseHolderEvidenceResponse>
        {
            new GetLeaseHolderEvidenceResponse
            {
                Id = firstEvidenceGuid,
                Name = "First Evidence",
                Extension = "ext",
                Size = 12345
            },
            new GetLeaseHolderEvidenceResponse
            {
                Id = secondEvidenceGuid,
                Name = "Second Evidence",
                Extension = "ext",
                Size = 6789
            },
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QueryAsync<GetLeaseHolderEvidenceResponse>("GetLeaseHolderEngagementFilesForApplication", It.IsAny<object>()))
                                .ReturnsAsync(evidenceResponse)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetLeaseHolderEvidenceRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.Count() == 2));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}

using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorList.GetAssessorList;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;


public class GetAccessorListTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IFireRiskAppraisalRepository> _fireAssessorListService;

    private readonly GetAssessorListHandler _handler;

    public GetAccessorListTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _fireAssessorListService = new Mock<IFireRiskAppraisalRepository>(MockBehavior.Strict);

        _handler = new GetAssessorListHandler(_connection.Object, _applicationDataProvider.Object,
                                              _fireAssessorListService.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange                
        var assessorList = new List<GetFireRiskAssessorListResult>
        {
            new GetFireRiskAssessorListResult
            {
                Id = 1,
                CompanyName = "First company",
                EmailAddress = "region1@test.com",
                Telephone = "01962 123456"
            },
            new GetFireRiskAssessorListResult
            {
                Id = 2,
                CompanyName = "Second company",
                EmailAddress = "region2@test.com",
                Telephone = "01962 789012"
            },
        };

        string referenceNo = "12345";
        Guid AppId = Guid.NewGuid();
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<string>("GetApplicationReferenceNumber", It.IsAny<object>()))
                                .ReturnsAsync(referenceNo)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(AppId)
                                .Verifiable();

        _fireAssessorListService.Setup(x => x.GetFireAssessorList())
                                .ReturnsAsync(assessorList)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetAssessorListRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.ApplicationId == AppId) &&
                           (result.ApplicationReferenceNumber == "12345") &&
                           (result.AssessorList.Count() == 2));

        Assert.True(resultValid);
        _connection.Verify();
        _fireAssessorListService.Verify();
        _applicationDataProvider.Verify();
    }
}

using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AppraisalSurveyDetails.GetAppraisalSurveyDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetAppraisalSurveyDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IFireRiskAppraisalRepository> _fireAssessorListService;

    private readonly GetAppraisalSurveyDetailsHandler _handler;

    public GetAppraisalSurveyDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _fireAssessorListService = new Mock<IFireRiskAppraisalRepository>(MockBehavior.Strict);

        _handler = new GetAppraisalSurveyDetailsHandler(_connection.Object, 
                                                        _applicationDataProvider.Object,
                                                        _fireAssessorListService.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        var accessorDetails = new GetAppraisalSurveyDetailsResponse()
        {
            FireRiskAssessorId = 1,
            DateOfInstruction = DateTime.Now,
            SurveyDate = DateTime.Now.AddDays(1),
            FireAccessorNotOnPanel = false
        };

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
        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetAppraisalSurveyDetailsResponse>("GetAppraisalSurveyDetails", It.IsAny<object>()))
                                .ReturnsAsync(accessorDetails)
                                .Verifiable();

        _fireAssessorListService.Setup(x => x.GetFireAssessorList())
                                .ReturnsAsync(assessorList)
                                .Verifiable();


        //// Act
        var result = await _handler.Handle(GetAppraisalSurveyDetailsRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.FireRiskAssessorId == 1) &&
                           (result.FireRiskAssessorCompanies.Count() == 2));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}

using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.SurveyInstructionDetails.GetSurveyInstructionDetails;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetSurveyInstructionDetailsTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IFireRiskAppraisalRepository> _fireAssessorListService;

    private readonly GetSurveyInstructionDetailsHandler _handler;

    public GetSurveyInstructionDetailsTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _fireAssessorListService = new Mock<IFireRiskAppraisalRepository>(MockBehavior.Strict);


        _handler = new GetSurveyInstructionDetailsHandler(_connection.Object, _applicationDataProvider.Object,
                                                          _fireAssessorListService.Object);
    }

    [Fact]
    public async Task Handler_Returns_Combined_Data_From_DB()
    {
        //Arrange        
        var surveyDetails = new GetSurveyInstructionDetailsResponse()
        { 
            FireRiskAssessorId = 1,
            DateOfInstruction = DateTime.Now
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
        
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetSurveyInstructionDetailsResponse>("GetSurveyInstructionDetails", It.IsAny<object>()))
                                .ReturnsAsync(surveyDetails)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _fireAssessorListService.Setup(x => x.GetFireAssessorList())
                                .ReturnsAsync(assessorList)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetSurveyInstructionDetailsRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.FireRiskAssessorId == 1) &&
                           (result.FireRiskAssessorCompanies.Count() == 2));        
        Assert.True(resultValid);
        _connection.Verify();
        _fireAssessorListService.Verify();
        _applicationDataProvider.Verify();
    }

    [Fact]
    public async Task Handler_Returns_Nothing_From_DB()
    {
        //Arrange        
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<GetSurveyInstructionDetailsResponse>("GetSurveyInstructionDetails", It.IsAny<object>()))
                                .ReturnsAsync(() => null)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _fireAssessorListService.Setup(x => x.GetFireAssessorList())
                                .ReturnsAsync(() => null)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetSurveyInstructionDetailsRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.FireRiskAssessorId == 0) &&
                           (result.FireRiskAssessorCompanies is null));        
        Assert.True(resultValid);
        _connection.Verify();
        _fireAssessorListService.Verify();
        _applicationDataProvider.Verify();
    }
}


using HE.Remediation.Core.Data;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.GetRecommendedWorks;
using Moq;
using System;
using System.Collections.ObjectModel;

namespace HE.Remediation.Core.Tests.UseCase.Areas.FireRiskAppraisal;

public class GetRecommendedWorksTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly GetRecommendedWorksHandler _handler;

    public GetRecommendedWorksTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _handler = new GetRecommendedWorksHandler(_connection.Object, _applicationDataProvider.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        FRAEWBuildingDetails buildingDetails = new FRAEWBuildingDetails
        {
            BuildingAddress = "10 High Street",
            FRAEWInstructedDate = new DateTime(2022, 8, 22),
            BuildingName = "My building",
            FRAEWCompletedDate = new DateTime(2022, 8, 24),
            CompanyUndertakingReport = "Undertakers"
        };

        FRAEWRecommendedWorkDetails workDetails = new FRAEWRecommendedWorkDetails
        {
            LifeSafetyRiskAssessment = ERiskType.Low,
            RecommendCladding = EReplacementCladding.Partial,
            RecommendBuildingIntetim = ENoYes.Yes,
            RecommendedTotalAreaCladding = 2,
            CaveatsLimitations = "no limitations",
            RemediationSummary = "empty summary?",
            JustifyRecommendation = "yes"
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<FRAEWBuildingDetails>("GetFRAEWBuildingDetails", It.IsAny<object>()))
                                .ReturnsAsync(buildingDetails)
                                .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<FRAEWRecommendedWorkDetails>("GetApplicationFireRiskRecommendedWorksDetails", It.IsAny<object>()))
                                .ReturnsAsync(workDetails)
                                .Verifiable();

        _connection.Setup(x => x.QueryAsync<EInterimMeasuresType>("GetRecommendedWorksInterimMeasures", It.IsAny<object>()))
            .ReturnsAsync(new List<EInterimMeasuresType> { EInterimMeasuresType.Other })
            .Verifiable();

        _connection.Setup(x => x.QueryAsync<ERiskSafetyMitigationType>("GetRecommendedWorksRiskMitigationResponsesForApplication", It.IsAny<object>()))
            .ReturnsAsync(new List<ERiskSafetyMitigationType> { ERiskSafetyMitigationType.Other })
            .Verifiable();

        //// Act
        var result = await _handler.Handle(GetRecommendedWorksRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);
        var resultValid = ((result != null) &&
                           (result.BuildingAddress == "10 High Street") &&
                           (result.CaveatsLimitations == "no limitations") &&
                           (result.JustifyRecommendation == "yes"));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}

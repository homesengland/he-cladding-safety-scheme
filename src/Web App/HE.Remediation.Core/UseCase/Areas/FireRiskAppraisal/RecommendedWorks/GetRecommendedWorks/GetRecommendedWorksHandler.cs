using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.GetRecommendedWorks;

public class GetRecommendedWorksHandler: IRequestHandler<GetRecommendedWorksRequest, GetRecommendedWorksResponse>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
        
    public GetRecommendedWorksHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetRecommendedWorksResponse> Handle(GetRecommendedWorksRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var buildingDetails = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<FRAEWBuildingDetails>("GetFRAEWBuildingDetails",
                                                                                                           new { applicationId });

        var recommendedDetails = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<FRAEWRecommendedWorkDetails>("GetApplicationFireRiskRecommendedWorksDetails",
                                                                                                           new { applicationId });

        if (recommendedDetails == null)
        {
            return new GetRecommendedWorksResponse
            {
                BuildingAddress = buildingDetails.BuildingAddress,
                FRAEWInstructedDate = buildingDetails.FRAEWInstructedDate,
                BuildingName = buildingDetails.BuildingName,
                FRAEWCompletedDate = buildingDetails.FRAEWCompletedDate,
                CompanyUndertakingReport = buildingDetails.CompanyUndertakingReport
            };
        }

        var response = new GetRecommendedWorksResponse
        {
            LifeSafetyRiskAssessment = recommendedDetails.LifeSafetyRiskAssessment,
            RecommendCladding = recommendedDetails.RecommendCladding,
            RecommendBuildingIntetim = recommendedDetails.RecommendBuildingIntetim,
            RecommendedTotalAreaCladding = recommendedDetails.RecommendedTotalAreaCladding,
            CaveatsLimitations = recommendedDetails.CaveatsLimitations,
            RemediationSummary = recommendedDetails.RemediationSummary,
            JustifyRecommendation = recommendedDetails.JustifyRecommendation,
            BuildingAddress = buildingDetails.BuildingAddress,
            FRAEWInstructedDate = buildingDetails.FRAEWInstructedDate,
            BuildingName = buildingDetails.BuildingName,
            FRAEWCompletedDate = buildingDetails.FRAEWCompletedDate,
            CompanyUndertakingReport = buildingDetails.CompanyUndertakingReport
        };

        return response;
    }
}

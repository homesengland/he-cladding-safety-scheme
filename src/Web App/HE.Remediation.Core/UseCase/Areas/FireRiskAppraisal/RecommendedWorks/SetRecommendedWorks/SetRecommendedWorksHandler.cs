using Dapper;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Data;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.SetRecommendedWorks;

public class SetRecommendedWorksHandler: IRequestHandler<SetRecommendedWorksRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _db;

    public SetRecommendedWorksHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
    {
        _applicationDataProvider = applicationDataProvider;
        _db = db;
    }

    public async Task<Unit> Handle(SetRecommendedWorksRequest request, CancellationToken cancellationToken)
    {
        await SetRecommendedWorkDetails(request);
        return Unit.Value;
    }

    private async Task SetRecommendedWorkDetails(SetRecommendedWorksRequest request)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var parms = new DynamicParameters();
            parms.Add("ApplicationId", applicationId);
            parms.Add("LifeSafetyRiskAssessment", request.LifeSafetyRiskAssessment);
            parms.Add("RecommendCladding", request.RecommendCladding);
            parms.Add("RecommendBuildingIntetim", request.RecommendBuildingIntetim);
            parms.Add("CaveatsLimitations", request.CaveatsLimitations);
            parms.Add("RemediationSummary", request.RemediationSummary);
            parms.Add("JustifyRecommendation", request.JustifyRecommendation);
            parms.Add("OtherInterimMeasuresText", request.OtherInterimMeasuresText);
            parms.Add("RiskMitigationOtherText", request.SafetyRiskOtherText);
            parms.Add("OtherRiskMitigationOptionsConsidered", request.OtherRiskMitigationOptionsConsidered);
            parms.Add("RecommendedWorksId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            await _db.ExecuteAsync("InsertOrUpdateApplicationFireRiskRecommendedWorksDetails", parms);

            var worksId = parms.Get<Guid>("RecommendedWorksId");

            await InsertInterimMeasureResponses(request, worksId);

            await InsertSafetyRiskMitigationResponses(request, worksId);

            scope.Complete();
        }
    }

    private async Task InsertInterimMeasureResponses(SetRecommendedWorksRequest request, Guid worksId)
    {
        var interimMeasureParams = new DynamicParameters();
        interimMeasureParams.Add("RecommendedWorksId", worksId);
        interimMeasureParams.Add("measures", request.RecommendedInterimMeasuresTypes.Select(x => (int)x)
            .ToDataTable()
            .AsTableValuedParameter("[dbo].[IntListType]"));
        await _db
            .ExecuteAsync("InsertUpdateRecommendedInterimMeasures", interimMeasureParams);
    }

    private async Task InsertSafetyRiskMitigationResponses(SetRecommendedWorksRequest request, Guid worksId)
    {
        var riskMitigationResponseParams = new DynamicParameters();
        riskMitigationResponseParams.Add("RecommendedWorksId", worksId);
        riskMitigationResponseParams.Add("RiskMitigationResponses", request.RiskSafetyMitigationTypes.Select(x => (int)x)
            .ToDataTable()
            .AsTableValuedParameter("[dbo].[IntListType]"));
        await _db
            .ExecuteAsync("InsertUpdateSafetyRiskMitigationResponses", riskMitigationResponseParams);
    }
}

using HE.Remediation.Core.Interface;
using MediatR;
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
            await _db.ExecuteAsync("InsertOrUpdateApplicationFireRiskRecommendedWorksDetails", new
            {
                applicationId,
                request.LifeSafetyRiskAssessment,
                request.RecommendCladding,
                request.RecommendBuildingIntetim,
                request.RecommendedTotalAreaCladding,
                request.CaveatsLimitations,
                request.RemediationSummary,
                request.JustifyRecommendation
            });

            scope.Complete();
        }
    }
}

using HE.Remediation.Core.Interface;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.SetReportDetails;

public class SetReportDetailsHandler : IRequestHandler<SetReportDetailsRequest, Unit>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _db;

    public SetReportDetailsHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
    {
        _applicationDataProvider = applicationDataProvider;
        _db = db;
    }

    public async ValueTask<Unit> Handle(SetReportDetailsRequest request, CancellationToken cancellationToken)
    {
        await SetReportDetails(request);
        return Unit.Value;
    }

    private async Task SetReportDetails(SetReportDetailsRequest request)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _db.ExecuteAsync("InsertOrUpdateFireRiskAssessmentReportDetails", new
            {
                applicationId,
                request.AuthorsName,
                request.PeerReviewPerson,
                request.FraewCost,
                request.CompanyUndertakingReport,
                request.NumberOfStoreys,
                request.BuildingHeight,
                request.BasicComplexId
            });

            scope.Complete();
        }
    }
}

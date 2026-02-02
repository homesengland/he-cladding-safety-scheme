using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance;

public class SetBuildingsInsuranceHandler(IApplicationDataProvider applicationDataProvider,
    IBuildingsInsuranceRepository buildingsInsuranceRepository, IClosingReportRepository closingReportRepository) : IRequestHandler<SetBuildingsInsuranceRequest, SetBuildingsInsuranceResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;
    private readonly IBuildingsInsuranceRepository _buildingsInsuranceRepository = buildingsInsuranceRepository;
    private readonly IClosingReportRepository _closingReportRepository = closingReportRepository;

    public async ValueTask<SetBuildingsInsuranceResponse> Handle(SetBuildingsInsuranceRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        request.ApplicationId = applicationId;

        SetBuildingsInsuranceResponse response;

        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            response = await _buildingsInsuranceRepository.SaveClosingReportBuildingInsurance(request);
            await _closingReportRepository.UpsertClosingReportTaskStatus(applicationId, EClosingReportTask.BuildingInsuranceInformation, ETaskStatus.Completed);
            transactionScope.Complete();
        }

        return response;
    }
}

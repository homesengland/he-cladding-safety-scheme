using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.SetInformedLeaseholder;

public class SetInformedLeaseholderHandler : IRequestHandler<SetInformedLeaseholderRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetInformedLeaseholderHandler(IApplicationDataProvider applicationDataProvider,
                                         IApplicationRepository applicationRepository,
                                         IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetInformedLeaseholderRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportVersion = await _progressReportingRepository.GetProgressReportVersion();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await UpdateLeaseholdersInformed(request);

        if (progressReportVersion == 1)
        {
            await _applicationRepository.UpdateInternalStatus(applicationId, EApplicationInternalStatus.PrimaryReportInProgress);
        }

        scope.Complete();

        return Unit.Value;
    }

    private async Task UpdateLeaseholdersInformed(SetInformedLeaseholderRequest request)
    {
        await _progressReportingRepository.UpdateLeaseholdersInformed(request.LeaseholdersInformed);        
    }
}

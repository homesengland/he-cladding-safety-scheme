using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Transactions;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.SetInformedLeaseholder;

public class SetInformedLeaseholderHandler : IRequestHandler<SetInformedLeaseholderRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetInformedLeaseholderHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingRepository progressReportingRepository, 
        IStatusTransitionService statusTransitionService)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<Unit> Handle(SetInformedLeaseholderRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportVersion = await _progressReportingRepository.GetProgressReportVersion();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await UpdateLeaseholdersInformed(request);

        if (progressReportVersion == 1)
        {
            await _statusTransitionService.TransitionToInternalStatus(EApplicationInternalStatus.PrimaryReportInProgress, applicationIds: applicationId);
        }

        scope.Complete();

        return Unit.Value;
    }

    private async Task UpdateLeaseholdersInformed(SetInformedLeaseholderRequest request)
    {
        await _progressReportingRepository.UpdateLeaseholdersInformed(request.LeaseholdersInformed);        
    }
}

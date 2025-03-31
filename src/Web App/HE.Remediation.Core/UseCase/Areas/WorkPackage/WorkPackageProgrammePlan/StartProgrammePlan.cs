using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;
public class StartProgrammePlanHandler : IRequestHandler<StartProgrammePlanRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageRepository _workPackageRepository;

    public StartProgrammePlanHandler(IApplicationDataProvider applicationDataProvider, IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(StartProgrammePlanRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _workPackageRepository.CreateWorkPackageProgrammePlan(applicationId);
        await _workPackageRepository.SetWorkPackageProgrammePlanTaskStatus(
            new SetWorkPackageProgrammePlanTaskStatusParameters
            {
                ApplicationId = applicationId,
                TaskStatusId = (int)ETaskStatus.InProgress
            });

        scope.Complete();

        return Unit.Value;
    }
}

public class StartProgrammePlanRequest : IRequest
{
    private StartProgrammePlanRequest()
    {
    }

    public static readonly StartProgrammePlanRequest Request = new();
}

using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;

public class SetAcquiredRightToManageHandler : IRequestHandler<SetAcquiredRightToManageRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IRightToManageRepository _rightToManageRepository;

    public SetAcquiredRightToManageHandler(IApplicationDataProvider applicationDataProvider, IRightToManageRepository rightToManageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _rightToManageRepository = rightToManageRepository;
    }

    public async Task<Unit> Handle(SetAcquiredRightToManageRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _rightToManageRepository.UpdateHasAcquiredRightToManage(new UpdateHasAcquiredRightToManageParameters
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            HasAcquiredRightToManage = request.HasAcquiredRightToManage!.Value
        });

        return Unit.Value;
    }
}

public class SetAcquiredRightToManageRequest : IRequest
{
    public bool? HasAcquiredRightToManage { get; set; }
}
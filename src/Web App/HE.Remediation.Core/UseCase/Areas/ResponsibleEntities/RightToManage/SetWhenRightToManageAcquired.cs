using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;

public class SetWhenRightToManageAcquiredHandler : IRequestHandler<SetWhenRightToManageAcquiredRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IRightToManageRepository _rightToManageRepository;

    public SetWhenRightToManageAcquiredHandler(IApplicationDataProvider applicationDataProvider, IRightToManageRepository rightToManageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _rightToManageRepository = rightToManageRepository;
    }

    public async ValueTask<Unit> Handle(SetWhenRightToManageAcquiredRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _rightToManageRepository.UpdateRightToManageAcquisition(new UpdateRightToManageAcquisitionParameters
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            RightToManageAcqusitionDate = request.RightToManageAcquisitionDate!.Value
        });

        return Unit.Value;
    }
}

public class SetWhenRightToManageAcquiredRequest : IRequest
{
    public DateTime? RightToManageAcquisitionDate { get; set; }
}
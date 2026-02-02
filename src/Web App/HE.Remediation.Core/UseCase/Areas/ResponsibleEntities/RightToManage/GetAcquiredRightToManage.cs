using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;

public class GetAcquiredRightToManageHandler : IRequestHandler<GetAcquiredRightToManageRequest, GetAcquiredRightToManageResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IRightToManageRepository _rightToManageRepository;

    public GetAcquiredRightToManageHandler(IApplicationDataProvider applicationDataProvider, IRightToManageRepository rightToManageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _rightToManageRepository = rightToManageRepository;
    }

    public async ValueTask<GetAcquiredRightToManageResponse> Handle(GetAcquiredRightToManageRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _rightToManageRepository.CreateRightToManageIfNotExists(applicationId);

        var hasAcquiredRightToManage = await _rightToManageRepository.GetHasAcquiredRightToManage(applicationId);

        return new GetAcquiredRightToManageResponse
        {
            HasAcquiredRightToManage = hasAcquiredRightToManage
        };
    }
}

public class GetAcquiredRightToManageRequest : IRequest<GetAcquiredRightToManageResponse>
{
    private GetAcquiredRightToManageRequest()
    {
    }

    public static readonly GetAcquiredRightToManageRequest Request = new ();
}

public class GetAcquiredRightToManageResponse
{
    public bool? HasAcquiredRightToManage { get; set; }
}
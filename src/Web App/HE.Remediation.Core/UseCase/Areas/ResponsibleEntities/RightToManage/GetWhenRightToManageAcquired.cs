using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;

public class GetWhenRightToManageAcquiredHandler : IRequestHandler<GetWhenRightToManageAcquiredRequest, GetWhenRightToManageAcquiredResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IRightToManageRepository _rightToManageRepository;

    public GetWhenRightToManageAcquiredHandler(IApplicationDataProvider applicationDataProvider, IRightToManageRepository rightToManageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _rightToManageRepository = rightToManageRepository;
    }

    public async ValueTask<GetWhenRightToManageAcquiredResponse> Handle(GetWhenRightToManageAcquiredRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var acquisitionDate = await _rightToManageRepository.GetRightToManageAcquisition(_applicationDataProvider.GetApplicationId());

        return new GetWhenRightToManageAcquiredResponse
        {
            RightToManageAcquisitionDate = acquisitionDate
        };
    }
}

public class GetWhenRightToManageAcquiredRequest : IRequest<GetWhenRightToManageAcquiredResponse>
{
    private GetWhenRightToManageAcquiredRequest()
    {
    }

    public static readonly GetWhenRightToManageAcquiredRequest Request = new ();
}

public class GetWhenRightToManageAcquiredResponse
{
    public DateTime? RightToManageAcquisitionDate { get; set; }
}
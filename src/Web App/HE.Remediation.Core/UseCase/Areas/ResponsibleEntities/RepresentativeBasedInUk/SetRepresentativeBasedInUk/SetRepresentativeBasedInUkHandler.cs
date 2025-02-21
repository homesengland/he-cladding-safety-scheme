using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.SetRepresentativeBasedInUk;

public class SetRepresentativeBasedInUkHandler : IRequestHandler<SetRepresentativeBasedInUkRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetRepresentativeBasedInUkHandler(
        IDbConnectionWrapper connection, 
        IApplicationDataProvider applicationDataProvider, 
        IStatusTransitionService statusTransitionService)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<Unit> Handle(SetRepresentativeBasedInUkRequest request, CancellationToken cancellationToken)
    {
        await SaveResponse(request.BasedInUk!.Value);
        return Unit.Value;
    }

    private async Task SaveResponse(bool isInUk)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _connection.ExecuteAsync("UpdateBasedInUk", new
        {
            ApplicationId = applicationId,
            IsBasedInUk = isInUk
        });

        await _statusTransitionService.TransitionToStatus(EApplicationStatus.ApplicationInProgress, applicationIds: applicationId);
    }
}
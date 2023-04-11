using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.NotEligible.SetNotEligible;

public class SetNotEligibleHandler : IRequestHandler<SetNotEligibleRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetNotEligibleHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetNotEligibleRequest request, CancellationToken cancellationToken)
    {
        await _connection.ExecuteAsync("UpdateApplicationDetailsStatus", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId(),
            StatusId = EApplicationStatus.NotEligible
        });
        return Unit.Value;
    }
}
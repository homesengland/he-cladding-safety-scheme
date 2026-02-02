using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.NewApplication;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.Dashboard.AcknowledgeNotification;

public class GetAcknowledgeNotificationHandler : IRequestHandler<GetAcknowledgeNotificationRequest, Unit>
{
    private readonly IDbConnectionWrapper _db;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetAcknowledgeNotificationHandler(IDbConnectionWrapper db, IApplicationDataProvider applicationDataProvider)
    {
        _db = db;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(GetAcknowledgeNotificationRequest request, CancellationToken cancellationToken)
    {
        await _db.ExecuteAsync("SetAlertAcknowledgement", new 
        { 
            AlertId = request.Id, 
            Acknowledged = true
        });

        var applicationId = await _db.QuerySingleOrDefaultAsync<Guid>("GetApplicationIdForAlert", new { AlertId = request.Id });
        var applicationDetails = await _db.QuerySingleOrDefaultAsync<UserIdEmailAddressAndSchemeId>("GetUserIdEmailAddressAndSchemeIdByApplicationId", new { applicationId });
        var applicationScheme = (EApplicationScheme)(applicationDetails.ApplicationSchemeId ?? (int)EApplicationScheme.CladdingSafetyScheme);
        
        _applicationDataProvider.SetApplicationDetails(applicationId, applicationScheme, applicationDetails.EmailAddress);

        return Unit.Value;
    }
}


public class GetAcknowledgeNotificationRequest: IRequest
{
    public Guid Id { get;set; }    
}

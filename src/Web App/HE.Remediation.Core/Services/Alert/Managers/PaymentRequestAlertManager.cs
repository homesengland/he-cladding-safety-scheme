using Azure.Core;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.Services.Alert.Managers;

public class PaymentRequestAlertManager : BuildingNameAlertManager
{
    public PaymentRequestAlertManager(IBuildingDetailsRepository buildingDetailsRepository)
    : base(buildingDetailsRepository)
    {
    }

    public override EAlertType AlertType => EAlertType.PaymentRequest;

    public override async Task<string> GetTitle(GetAlertsResult alert)
    {
        var buildingName = await GetBuildingName(alert.ApplicationId);
        return $"Payment request for {buildingName} is now due.";
    }
}
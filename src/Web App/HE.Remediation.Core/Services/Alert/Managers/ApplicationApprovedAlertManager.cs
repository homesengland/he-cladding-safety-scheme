using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Alert.Managers;

public class ApplicationApprovedAlertManager : BuildingNameAlertManager
{
    public ApplicationApprovedAlertManager(IBuildingDetailsRepository buildingDetailsRepository)
    : base(buildingDetailsRepository)
    {
    }

    public override EAlertType AlertType => EAlertType.ApplicationApproved;

    public override async Task<string> GetTitle(GetAlertsResult alert)
    {
        var buildingName = await GetBuildingName(alert.ApplicationId);
        return $"Your application for {buildingName} has been successful. You can now begin the pre-tender process.";
    }
}
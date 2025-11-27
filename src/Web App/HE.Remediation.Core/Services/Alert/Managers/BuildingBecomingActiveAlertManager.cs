using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Alert.Managers;

public class BuildingBecomingActiveAlertManager : BuildingNameAlertManager
{
    public BuildingBecomingActiveAlertManager(IBuildingDetailsRepository buildingDetailsRepository) : base(buildingDetailsRepository)
    {
    }

    public override EAlertType AlertType => EAlertType.BuildingBecomingActiveAlert;

    public override async Task<string> GetTitle(GetAlertsResult alert)
    {
        var buildingName = await GetBuildingName(alert.ApplicationId);
        return $"Your application for {buildingName} will become active in 30 days";
    }
}
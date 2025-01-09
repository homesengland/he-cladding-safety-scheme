using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Alert.Managers;

public class ScheduleOfWorksAlertManager : BuildingNameAlertManager
{
    public ScheduleOfWorksAlertManager(IBuildingDetailsRepository buildingDetailsRepository)
    : base(buildingDetailsRepository)
    {
    }

    public override EAlertType AlertType => EAlertType.ScheduleOfWorks;

    public override async Task<string> GetTitle(GetAlertsResult alert)
    {
        var buildingName = await GetBuildingName(alert.ApplicationId);
        return $"Your schedule of works for {buildingName} is now available to complete.";        
    }
}
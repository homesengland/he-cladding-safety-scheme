using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Alert.Managers;

public class WorksPackageAlertManager : BuildingNameAlertManager
{
    public WorksPackageAlertManager(IBuildingDetailsRepository buildingDetailsRepository)
    : base(buildingDetailsRepository)
    {
    }

    public override EAlertType AlertType => EAlertType.WorksPackage;

    public override async Task<string> GetTitle(GetAlertsResult alert)
    {
        var buildingName = await GetBuildingName(alert.ApplicationId);
        return $"Compile works package for {buildingName} is now available to complete.";
    }
}
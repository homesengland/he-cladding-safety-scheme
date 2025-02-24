using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Services.Alert.Managers;

public class ApplicationDetailsChangedAlertManager : BuildingNameAlertManager
{
    public ApplicationDetailsChangedAlertManager(IBuildingDetailsRepository buildingDetailsRepository) : base(buildingDetailsRepository)
    {
    }

    public override EAlertType AlertType => EAlertType.ApplicationDetailsChanged;

    public override async Task<string> GetTitle(GetAlertsResult alert)
    {
        var buildingName = await GetBuildingName(alert.ApplicationId);
        return $"Following your recent communications with Homes England, the application details for {buildingName} have been updated. " +
               $"For further information please contact <a class=\"govuk-link\" href=\"/Application/Dashboard/GetHelp\">Homes England</a>.";
    }

}
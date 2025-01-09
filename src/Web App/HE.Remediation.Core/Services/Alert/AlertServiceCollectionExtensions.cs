using HE.Remediation.Core.Services.Alert.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace HE.Remediation.Core.Services.Alert;

public static class AlertServiceCollectionExtensions
{
    public static void ConfigureAlertServices(this IServiceCollection services)
    {
        services.AddScoped<IAlertService, AlertService>();
        services.RegisterManagers();
    }

    private static void RegisterManagers(this IServiceCollection services)
    {
        var managerInterface = typeof(IAlertManager);
        var assembly = managerInterface.Assembly;
        var implementations = assembly.DefinedTypes
            .Where(t => t.Name.EndsWith("AlertManager") && !t.IsAbstract && t.IsAssignableTo(managerInterface));

        foreach (var implementation in implementations)
        {
            services.AddScoped(managerInterface, implementation);
        }
    }
}
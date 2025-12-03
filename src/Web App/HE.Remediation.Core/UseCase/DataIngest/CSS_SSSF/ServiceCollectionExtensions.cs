using Microsoft.Extensions.DependencyInjection;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.DataImporters;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.Lookups;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF.Validation;

namespace HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataImportForCss_Sssf(this IServiceCollection services)
    {
        services.AddScoped<IAddressResolver, AddressResolver>();
        services.AddTransient<JsonDataIngestMapperIValidator>();
        services.AddScoped<IBuildingDetailsDataImporter, BuildingDetailsDataImporter>();
        services.AddScoped<IResponsibleEntityDataImporter, ResponsibleEntityDataImporter>();
        services.AddScoped<IFraDataImporter, FraDataImporter>();
        services.AddScoped<IDataIngestionLookupService, DataIngestionLookupService>();

        return services;
    }
}

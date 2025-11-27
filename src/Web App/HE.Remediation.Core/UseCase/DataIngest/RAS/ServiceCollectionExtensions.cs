using Microsoft.Extensions.DependencyInjection;
using HE.Remediation.Core.UseCase.DataIngest.RAS.DataImporters;
using HE.Remediation.Core.UseCase.DataIngest.RAS.Lookups;
using HE.Remediation.Core.UseCase.DataIngest.RAS.Validation;

namespace HE.Remediation.Core.UseCase.DataIngest.RAS;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataImportForRas(this IServiceCollection services)
    {
        services.AddTransient<JsonDataIngestMapperIValidator>();
        services.AddScoped<IBuildingDetailsDataImporter, BuildingDetailsDataImporter>();
        services.AddScoped<IResponsibleEntityDataImporter, ResponsibleEntityDataImporter>();
        services.AddScoped<IFraDataImporter, FraDataImporter>();
        services.AddScoped<IFraewDataImporter, FraewDataImporter>();
        services.AddScoped<IDataIngestionLookupService, DataIngestionLookupService>();

        return services;
    }
}

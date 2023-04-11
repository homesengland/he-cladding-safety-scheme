using HE.Remediation.Core.Data;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Services.SessionTimeout;
using HE.Remediation.Core.Services.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HE.Remediation.Core.Settings;
using VirusScanner.Client.Interfaces;
using VirusScanner.Client.Services;
using Microsoft.AspNetCore.StaticFiles;
using HE.Remediation.Core.Data.Repositories;

namespace HE.Remediation.Core.IoC
{
    public static class ServicesDependencies
    {
        public static void AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnectionWrapper, DbConnectionWrapper>();
            services.AddScoped<IApplicationDataProvider, ApplicationDataProvider>();

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICustomFileFormatInspector, CustomFileFormatInspector>();
            services.AddScoped<IFireAssessorListRepository, FireAssessorListRepository>();
            services.AddScoped<IResponsibleEntityRepository, ResponsibleEntityRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUserService, UserService>();

            services.Configure<FileServiceSettings>(configuration.GetSection(nameof(FileServiceSettings)));
            services.Configure<VirusScanningSettings>(configuration);
            services.Configure<AwsS3Options>(configuration);

            services.AddScoped<IContentTypeProvider, FileExtensionContentTypeProvider>();
            services.AddScoped<IVirusScannerClient, VirusScannerClient>();
            services.AddHttpClient<IVirusScannerClient, VirusScannerClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("VIRUS_SCANNING_SERVER"));
            });

            var sessionTimeoutConfig = configuration.GetSection("SESSION_TIMEOUT_MINUTES").Value;
            services.AddSingleton(new SessionTimeout
            {
                Minutes = int.Parse(sessionTimeoutConfig ?? throw new InvalidOperationException()),
            });
        }
    }
}
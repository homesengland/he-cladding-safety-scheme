using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Auth0.AspNetCore.Authentication;
using HE.Remediation.Core.IoC;
using HE.Remediation.Core.Middleware;
using HE.Remediation.Core.Services.OidcEventHandlerService;
using HE.Remediation.Core.TypeHandlers;
using MediatR;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Licensing;
using System.Data;
using System.Reflection;
using HE.Remediation.Core.Services.GovNotify;
using HE.Remediation.Core.Services.GovNotify.Models;
using HE.Remediation.Core.Services.Communication;
using HE.Remediation.Core.Services.Communication.Collaboration;
using HE.Remediation.Core.Services.DataIngestion;
using HE.Remediation.Core.Settings;
using System.Configuration;
using HE.Remediation.Core.UseCase.DataIngest.Validation;
using HE.Remediation.Core.UseCase.DataIngest.Lookups;
using HE.Remediation.Core.UseCase.DataIngest.DataImporters;

namespace HE.Remediation.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration["DB_CONNSTRING"]
                                    ?? throw new InvalidOperationException("The DB_CONNSTRING configuration value has not been specified");

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IDbConnection>(e => new SqlConnection(connectionString));


            var awsOptions = new AWSOptions
            {
                Profile = Environment.GetEnvironmentVariable("AWS_PROFILE"),
                Region = Amazon.RegionEndpoint.EUWest2
            };

            var test = builder.Configuration.GetAWSOptions();

            builder.Services.AddDefaultAWSOptions(awsOptions);
            builder.Services.AddAWSService<IAmazonS3>();

            builder.Services.AddHealthChecks()
                .AddCheck<ReadyHealthCheck>("ready");

            services.AddServicesDependencies(builder.Configuration);

            builder.Services.AddSingleton<IBackgroundEmailCommunicationQueue, BackgroundEmailCommunicationQueue>();
            builder.Services.AddHostedService<EmailCommunicationHostedService>();

            builder.Services.AddSingleton<IBackgroundCollaborationCommunicationQueue, BackgroundCollaborationCommunicationQueue>();
            builder.Services.AddHostedService<CollaborationCommunicationHostedService>();

            builder.Services.AddSingleton<CommunicationConstants>();

            services.AddGovNotifyService(builder.Configuration);

            // Data Ingestion
            services.Configure<DataIngestionOptions>(builder.Configuration.GetSection("DataIngestion"));
            services.AddScoped<DataIngestionBatchChannel>();
            services.AddScoped<DataIngestionProducer>();
            services.AddScoped<DataIngestionConsumer>();
            services.AddHostedService<DataIngestionBackgroundService>();
            services.AddScoped<IAddressResolver, AddressResolver>();
            services.AddTransient<JsonDataIngestMapperIValidator>();
            services.AddScoped<IBuildingDetailsDataImporter, BuildingDetailsDataImporter>();
            services.AddScoped<IResponsibleEntityDataImporter, ResponsibleEntityDataImporter>();
            services.AddScoped<IDataIngestionLookupService, DataIngestionLookupService>();

            services.AddAuth0WebAppAuthentication(options =>
            {
                options.Domain = builder.Configuration["AUTH0_DOMAIN"]
                                    ?? throw new InvalidOperationException("The AUTH0_DOMAIN configuration value has not been specified");
                options.ClientId = builder.Configuration["AUTH0_CLIENTID"]
                                    ?? throw new InvalidOperationException("The AUTH0_CLIENTID configuration value has not been specified");
                options.OpenIdConnectEvents = new OpenIdConnectEvents
                {
                    OnRemoteFailure = OidcEventHandlerService.HandleRemoteFailureError
                };
            });

            services.AddAntiforgery(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            TypeHandlerRegistry.RegisterHandlers();

            SyncfusionLicenseProvider.RegisterLicense("NRAiBiAaIQQuGjN/V0Z+WE9EaFxKVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdUVlW3hfcXRXQ2FfVER1");
        }

        private static IServiceCollection AddGovNotifyService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<GovNotifySettings>(configuration.GetSection(nameof(GovNotifySettings)));

            services.AddScoped<IGovNotifyService, GovNotifyService>();

            services.AddHttpClient(GovNotifyServiceConstants.GovNotifyHttpClientName, client =>
                {
                    client.BaseAddress = new Uri(configuration["BASE_APIM_ENDPOINT"]
                                                 ?? throw new InvalidOperationException(
                                                     "The BASE_APIM_ENDPOINT configuration value has not been specified"));

                    client.DefaultRequestHeaders.Add("X-APIM-PROXY-KEY",
                        Environment.GetEnvironmentVariable("APIM_PROXY_API_KEY")
                        ?? throw new InvalidOperationException(
                            "The APIM_PROXY_API_KEY configuration value has not been specified"));
                }
            ).AddStandardResilienceHandler();

            return services;
        }
    }
}
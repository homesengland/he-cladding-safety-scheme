using Auth0.AspNetCore.Authentication;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using HE.Remediation.Core.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection;
using HE.Remediation.Core.Middleware;
using HE.Remediation.Core.Services.OidcEventHandlerService;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;

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

            services.AddAuth0WebAppAuthentication(options => {
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
        }
    }
}
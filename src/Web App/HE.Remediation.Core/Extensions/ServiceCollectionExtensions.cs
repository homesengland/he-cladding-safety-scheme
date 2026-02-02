using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Auth0.AspNetCore.Authentication;
using HE.Remediation.Core.IoC;
using HE.Remediation.Core.Middleware;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using HE.Remediation.Core.Services.Communication;
using HE.Remediation.Core.Services.Communication.Collaboration;
using HE.Remediation.Core.Services.DataIngestion;
using HE.Remediation.Core.Services.GovNotify;
using HE.Remediation.Core.Services.GovNotify.Models;
using HE.Remediation.Core.Services.OidcEventHandlerService;
using HE.Remediation.Core.Services.OpenTelemetry;
using HE.Remediation.Core.Settings;
using HE.Remediation.Core.TypeHandlers;
using HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF;
using HE.Remediation.Core.UseCase.DataIngest.RAS;
using Mediator;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Syncfusion.Licensing;
using System.Data;
using System.Reflection;

namespace HE.Remediation.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration["DB_CONNSTRING"]
                                    ?? throw new InvalidOperationException("The DB_CONNSTRING configuration value has not been specified");

            services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Transient;
                options.Assemblies = [typeof(ServiceCollectionExtensions).Assembly];
            });

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
            services.AddScoped<IApplicationDetailsProvider, ApplicationDetailsProvider>();

            builder.Services.AddSingleton<IBackgroundEmailCommunicationQueue, BackgroundEmailCommunicationQueue>();
            builder.Services.AddHostedService<EmailCommunicationHostedService>();

            builder.Services.AddSingleton<IBackgroundCollaborationCommunicationQueue, BackgroundCollaborationCommunicationQueue>();
            builder.Services.AddHostedService<CollaborationCommunicationHostedService>();

            builder.Services.AddSingleton<CommunicationConstants>();

            services.AddGovNotifyService(builder.Configuration);

            ConfigureOpenTelemetryServices(builder);

            // Data Ingestion
            services.Configure<DataIngestionOptions>(builder.Configuration.GetSection("DataIngestion"));
            services.AddScoped<DataIngestionBatchChannel>();
            services.AddScoped<DataIngestionProducer>();
            services.AddScoped<DataIngestionConsumer>();
            services.AddHostedService<DataIngestionBackgroundService>();
            services.AddDataImportForCss_Sssf();
            services.AddDataImportForRas();

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

        private static void ConfigureOpenTelemetryServices(WebApplicationBuilder builder)
        {
            var configBuilder = new OpenTelemetryConfigurationBuilder();
            var options = configBuilder.BuildOptions(
                key => builder.Configuration[key],
                Environment.GetEnvironmentVariable
            );

            ConfigureOpenTelemetryServices(builder, options);
        }

        internal static void ConfigureOpenTelemetryServices(WebApplicationBuilder builder, OpenTelemetryOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.OtelExporterEndpoint))
                return;

            var compositePropagator = new CompositeTextMapPropagator(new TextMapPropagator[]
            {
                new TraceContextPropagator(),                          // W3C Trace Context (standard, X-Ray compatible)
                new BaggagePropagator(),                               // W3C Baggage for custom attributes (original path tracking)
                new OpenTelemetry.Extensions.Propagators.B3Propagator() // B3 format for broader compatibility
            });
            
            Sdk.SetDefaultTextMapPropagator(compositePropagator);

            builder.Services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                {
                    var resourceBuilder = ResourceBuilder.CreateDefault()
                        .AddService(
                            serviceName: options.ServiceName,
                            serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0",
                            serviceInstanceId: Environment.MachineName)
                        .AddAttributes(new[]
                        {
                            new KeyValuePair<string, object>("host.name", Environment.MachineName),
                            new KeyValuePair<string, object>("process.runtime.name", ".NET"),
                            new KeyValuePair<string, object>("process.runtime.version", Environment.Version.ToString())
                        });

                    if (options.ResourceAttributes.Any())
                    {
                        var attributes = options.ResourceAttributes
                            .Select(kvp => new KeyValuePair<string, object>(kvp.Key, kvp.Value))
                            .ToArray();
                        
                        resourceBuilder = resourceBuilder.AddAttributes(attributes);
                    }

                    tracerProviderBuilder
                        .SetResourceBuilder(resourceBuilder)
                        .AddXRayTraceId()
                        .SetSampler(new ParentBasedSampler(new AlwaysSamplePostRequestsSampler(options.SamplingRatio)))
                        .AddAspNetCoreInstrumentation(aspNetCoreOptions =>
                        {
                            aspNetCoreOptions.RecordException = true;

                            EnrichHttpRequest(aspNetCoreOptions);
                        })
                        .AddHttpClientInstrumentation(httpOptions =>
                        {
                            httpOptions.RecordException = true;
                            
                            httpOptions.EnrichWithHttpRequestMessage = (activity, httpRequestMessage) =>
                            {
                                activity.SetTag("http.request.method", httpRequestMessage.Method.ToString());
                                if (httpRequestMessage.Content != null)
                                {
                                    activity.SetTag("http.request.has_content", true);
                                    var contentType = httpRequestMessage.Content.Headers.ContentType?.ToString();
                                    if (!string.IsNullOrEmpty(contentType))
                                    {
                                        activity.SetTag("http.request.content_type", contentType);
                                    }
                                }
                            };

                            httpOptions.EnrichWithHttpResponseMessage = (activity, httpResponseMessage) =>
                            {
                                activity.SetTag("http.response.reason_phrase", httpResponseMessage.ReasonPhrase);
                                if (httpResponseMessage.Content != null)
                                {
                                    var contentLength = httpResponseMessage.Content.Headers.ContentLength;
                                    if (contentLength.HasValue)
                                    {
                                        activity.SetTag("http.response.content_length", contentLength.Value);
                                    }
                                }
                            };
                    });

                // Only add SQL instrumentation if not suppressed
                // Suppressing prevents database from appearing as separate node in X-Ray
                if (!options.SuppressSqlInstrumentation)
                {
                    tracerProviderBuilder.AddSqlClientInstrumentation(sqlOptions =>
                    {
                        // Always record exceptions
                        sqlOptions.RecordException = true;

                            // Enhanced enrichment with additional metadata
                            if (options.EnableEnhancedSqlEnrichment)
                            {
                                sqlOptions.EnrichWithSqlCommand = (activity, sqlCommand) =>
                                {
                                    if (sqlCommand is SqlCommand cmd)
                                    {
                                        // Command timeout - helps identify timeout issues
                                        activity.SetTag("db.command.timeout", cmd.CommandTimeout);
                                        
                                        // Parameter count - indicator of query complexity
                                        activity.SetTag("db.command.parameters.count", cmd.Parameters.Count);
                                        
                                        // Command type
                                        activity.SetTag("db.command.type", cmd.CommandType.ToString());
                                        
                                        // For stored procedures, add the procedure name
                                        if (cmd.CommandType == CommandType.StoredProcedure && options.SetDbStatementForStoredProcedure)
                                        {
                                            activity.SetTag("db.statement", cmd.CommandText);
                                            activity.SetTag("db.operation", cmd.CommandText);
                                        }
                                        
                                        // For text commands, optionally add statement (be careful with PII)
                                        if (cmd.CommandType == CommandType.Text && options.SetDbStatementForText)
                                        {
                                            // Truncate long queries to avoid excessive data
                                            var statement = cmd.CommandText;
                                            if (statement?.Length > 1000)
                                            {
                                                statement = statement.Substring(0, 1000) + "... [truncated]";
                                            }
                                            activity.SetTag("db.statement", statement);
                                        }
                                        
                                        // Transaction isolation level if available
                                        if (cmd.Transaction != null)
                                        {
                                            activity.SetTag("db.transaction.isolation_level", cmd.Transaction.IsolationLevel.ToString());
                                        }
                                    }
                                };
                            }
                            else if (options.SetDbStatementForStoredProcedure)
                            {
                                // Legacy: Keep existing behavior if enhanced enrichment is disabled
                                sqlOptions.EnrichWithSqlCommand = (activity, sqlCommand) =>
                                {
                                    if (sqlCommand is SqlCommand cmd && cmd.CommandType == CommandType.StoredProcedure)
                                    {
                                        activity.SetTag("db.statement", cmd.CommandText);
                                    }
                                };
                            }

                            // Filter support to exclude specific queries
                            if (options.EnableSqlFiltering && options.SqlFilterExcludePatterns?.Length > 0)
                            {
                                sqlOptions.Filter = cmd =>
                                {
                                    if (cmd is not SqlCommand sqlCommand)
                                        return true; // Include if we can't determine

                                    var commandText = sqlCommand.CommandText;
                                    if (string.IsNullOrWhiteSpace(commandText))
                                        return true;

                                    // Exclude if matches any exclude pattern
                                    foreach (var pattern in options.SqlFilterExcludePatterns)
                                    {
                                        if (commandText.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                                        {
                                            return false; // Exclude this query
                                        }
                                    }

                                    return true; // Include by default
                                };
                            }
                        });
                    }

                    tracerProviderBuilder
                        .AddProcessor(new DatabaseAsSubsegmentProcessor())
                        .AddProcessor(new ExceptionAlwaysExportProcessor())
                        .AddOtlpExporter(otlpOptions =>
                        {
                            otlpOptions.Endpoint = new Uri(options.OtelExporterEndpoint);
                            
                            if (!Enum.TryParse<OpenTelemetry.ExportProcessorType>(options.ExportProcessorType, out var exportProcessorType))
                            {
                                exportProcessorType = OpenTelemetry.ExportProcessorType.Batch;
                            }
                            
                            otlpOptions.ExportProcessorType = exportProcessorType;
                        });

                    if (options.EnableConsoleExporter)
                    {
                        tracerProviderBuilder.AddConsoleExporter();
                    }
                });

            // Metrics support for SqlClient (optional)
            if (options.EnableSqlMetrics)
            {
                builder.Services.AddOpenTelemetry()
                    .WithMetrics(meterProviderBuilder =>
                    {
                        meterProviderBuilder
                            .SetResourceBuilder(ResourceBuilder.CreateDefault()
                                .AddService(
                                    serviceName: options.ServiceName,
                                    serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0"))
                            .AddSqlClientInstrumentation()
                            .AddOtlpExporter(otlpOptions =>
                            {
                                otlpOptions.Endpoint = new Uri(options.OtelExporterEndpoint);
                            });

                        if (options.EnableConsoleExporter)
                        {
                            meterProviderBuilder.AddConsoleExporter();
                        }
                    });
            }
        }

        private static void EnrichHttpRequest(OpenTelemetry.Instrumentation.AspNetCore.AspNetCoreTraceInstrumentationOptions aspNetCoreOptions)
        {
            aspNetCoreOptions.EnrichWithHttpRequest = (activity, httpRequest) =>
            {
                var referer = httpRequest.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(referer))
                {
                    activity.SetTag("http.referer", referer);
                }

                var origin = httpRequest.Headers["Origin"].ToString();
                if (!string.IsNullOrEmpty(origin))
                {
                    activity.SetTag("http.origin", origin);
                }

                var userAgent = httpRequest.Headers["User-Agent"].ToString();
                if (!string.IsNullOrEmpty(userAgent))
                {
                    activity.SetTag("http.user_agent", userAgent);
                }

                var contentType = httpRequest.ContentType;
                if (!string.IsNullOrEmpty(contentType))
                {
                    activity.SetTag("http.request.content_type", contentType);
                }

                if (httpRequest.QueryString.HasValue)
                {
                    activity.SetTag("http.query_string", httpRequest.QueryString.Value);
                }

                var clientIp = httpRequest.HttpContext.Connection.RemoteIpAddress?.ToString();
                if (!string.IsNullOrEmpty(clientIp))
                {
                    activity.SetTag("client.ip", clientIp);
                }

                if (httpRequest.HttpContext.User?.Identity?.IsAuthenticated == true)
                {
                    var userId = httpRequest.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        activity.SetTag("user.id", userId);
                    }

                    var userName = httpRequest.HttpContext.User.Identity.Name;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        activity.SetTag("user.name", userName);
                    }
                }

                var endpoint = httpRequest.HttpContext.GetEndpoint();
                if (endpoint != null)
                {
                    var routePattern = endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Routing.RouteEndpoint>()?.RoutePattern?.RawText;
                    if (!string.IsNullOrEmpty(routePattern))
                    {
                        activity.SetTag("http.route", routePattern);
                    }
                }
            };

            aspNetCoreOptions.EnrichWithHttpResponse = (activity, httpResponse) =>
            {
                if (!string.IsNullOrEmpty(httpResponse.ContentType))
                {
                    activity.SetTag("http.response.content_type", httpResponse.ContentType);
                }
                activity.SetTag("http.response.content_length", (httpResponse.ContentLength ?? 0).ToString());
            };
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
        
        /// <summary>
        /// Adds middleware to track original request paths before error handling redirects.
        /// This ensures that when errors occur and requests are redirected to /error,
        /// the trace still contains the original failing endpoint information.
        /// </summary>
        public static IApplicationBuilder UseOriginalRequestTracking(this IApplicationBuilder app)
        {
            return app.UseMiddleware<OriginalRequestTrackingMiddleware>();
        }
    }
}
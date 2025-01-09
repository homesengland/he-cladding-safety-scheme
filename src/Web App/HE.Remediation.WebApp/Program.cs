using FluentValidation;
using FluentValidation.AspNetCore;
using GovUk.Frontend.AspNetCore;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Middleware;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Razor;
using Polly;

namespace HE.Remediation.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddSerilogLogging();

            ConfigureServices(builder);

            var app = builder.Build();

            ConfigurePipeline(app);

            app.Run();
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            var coreAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(x => x.GetName().Name == "HE.Remediation.Core");

            builder.Services.AddDataProtection().PersistKeysToDatabase();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssembly(coreAssembly);
            builder.Services.AddGovUkFrontend();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddCoreServices(builder);

            builder.Services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Insert(0,"/Areas/WorksPackage/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Insert(1,"/Areas/WorksPackage/Views/Shared/{0}.cshtml");
            });

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            }
            else
            {
                builder.Services.AddControllersWithViews();
            }

            builder.Services.AddHttpContextAccessor();

            int retryCount = Int32.Parse(Environment.GetEnvironmentVariable("APIM_RETRY_COUNT") ?? "5");
            int delayBetweenRetries = Int32.Parse(Environment.GetEnvironmentVariable("APIM_DELAY_BETWEEN_RETRY_MS") ?? "500");

            builder.Services.AddHttpClient("ApimClient", apimClient =>
            {
                apimClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BASE_APIM_ENDPOINT"));

                apimClient.DefaultRequestHeaders.Add("X-APIM-PROXY-KEY",
                    Environment.GetEnvironmentVariable("APIM_PROXY_API_KEY"));
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(2))
            .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(retryCount,
                                                                              retryNumber => TimeSpan.FromMilliseconds(delayBetweenRetries)));
            
            var mvcBuilder = builder.Services.AddMvc();
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        public static void ConfigurePipeline(WebApplication app)
        {
            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next(context);
            });

            if (!app.Environment.IsDevelopment())
            {
                app.UseForwardedHeaders();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/Error/HandleError/{0}");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseProfileCompletionMiddleware();

            app.MapHealthChecks("/health");

            var applicationAreas = new[]
            {
                "Application", 
                "AlternativeFundingRoutes", 
                "Leaseholder", 
                "FireRiskAppraisal",
                "PreTenderSupport",
                "ProgressReporting",
                "Administration",
                "ScheduleOfWorks",
                "ApprovedScheduleOfWorks",                
                "PaymentRequest",
                "VariationRequest"
            };

            var workPackageAreas = new[]
            {
                "WorksPackage", 
                "WorksPackageProjectTeam",
                "WorksPackageGrantCertifyingOfficer", 
                "WorksPackageDutyOfCareDeed",
                "WorksPackagePlanningPermission",
                "WorksPackageCostsScheduling", 
                "WorksPackageKeyDates",
                "WorksPackageSignatories", 
                "WorksPackageDeclaration",
                "WorksPackageSubmit"
            };
            foreach (var areaName in applicationAreas.Union(workPackageAreas))
            {
                app.MapAreaControllerRoute(
                    name: areaName,
                    areaName: areaName,
                    pattern: areaName + "/{controller=Start}/{action=Index}/{id?}");
            }
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Application}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
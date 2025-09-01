using FluentValidation;
using FluentValidation.AspNetCore;
using GovUk.Frontend.AspNetCore;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Middleware;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Razor;

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
                options.AreaViewLocationFormats.Insert(2, "/Areas/ClosingReport/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Insert(3, "/Areas/ClosingReport/Views/Shared/{0}.cshtml");
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

            builder.Services.AddHttpClient("ApimClient", apimClient =>
            {
                apimClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BASE_APIM_ENDPOINT"));

                apimClient.DefaultRequestHeaders.Add("X-APIM-PROXY-KEY",
                    Environment.GetEnvironmentVariable("APIM_PROXY_API_KEY"));
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(2))
            .AddStandardResilienceHandler();
            
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

            app.UseExceptionLoggingMiddleware();
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
                "WorksPackageSubmit",
                "WorksPackageProgrammePlan",
                "WorksPackageFireRiskAssessment"
            };

            var closingReportAreas = new[]
            {
                "ClosingReport",
                "ClosingReportBuildingControlEvidence",
                "ClosingReportBuildingsInsurance",
                "ClosingReportFinalCost",
                "ClosingReportFinalPaymentRequest",
                "ClosingReportFireRiskAssessment",
                "ClosingReportLeaseholderCommunication",
                "ClosingReportPracticalCompletionCertificate",
                "ClosingReportSubmission",
                "ClosingReportEvidenceOfThirdPartyContribution"
            };

            foreach (var areaName in applicationAreas.Union(workPackageAreas).Union(closingReportAreas))
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
using FluentValidation;
using FluentValidation.AspNetCore;
using GovUk.Frontend.AspNetCore;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Middleware;
using Microsoft.AspNetCore.HttpOverrides;

namespace HE.Remediation.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.TraversePath().Load();
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
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();


            var mvcBuilder = builder.Services.AddMvc();
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            if (builder.Environment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
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

            app.MapAreaControllerRoute(
                name: "ApplicationArea",
                areaName: "Application",
                pattern: "Application/{controller=Start}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "AlternativeFundingRoutesArea",
                areaName: "AlternativeFundingRoutes",
                pattern: "AlternativeFundingRoutes/{controller=Start}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "RegisteredProvider",
                areaName: "RegisteredProvider",
                pattern: "RegisteredProvider/{controller=Start}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "FireRiskAppraisalArea",
                areaName: "FireRiskAppraisal",
                pattern: "FireRiskAppraisal/{controller=Start}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "AdministrationArea",
                areaName: "Administration",
                pattern: "Administration/{controller=Start}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Application}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
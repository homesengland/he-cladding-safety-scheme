using FileSignatures;
using HE.Remediation.Core.Data;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers;
using HE.Remediation.Core.Services.Alert;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Services.Location;
using HE.Remediation.Core.Services.SessionTimeout;
using HE.Remediation.Core.Services.UserService;
using HE.Remediation.Core.Settings;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using HE.Remediation.Core.Services.PdfRendererService;
using HE.Remediation.Core.Services.RazorRenderer;
using VirusScanner.Client.Interfaces;
using VirusScanner.Client.Services;
using HE.Remediation.Core.Services.StatusTransition;

namespace HE.Remediation.Core.IoC;

public static class ServicesDependencies
{
    public static void AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnectionWrapper, DbConnectionWrapper>();
        services.AddScoped<IApplicationDataProvider, ApplicationDataProvider>();
        services.AddScoped<IAnalyticsDataProivder, AnalyticsDataProvider>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.RegisterRepositories();
        services.ConfigureAlertServices();

        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ICustomFileFormatInspector, CustomFileFormatInspector>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<Services.Communication.ICommunicationService, Services.Communication.CommunicationService>();
        services.AddScoped<Services.Communication.Collaboration.ICommunicationService, Services.Communication.Collaboration.CommunicationService>();
        services.AddScoped<IPostCodeLookup, PostCodeLookup>();
        services.AddScoped<IRazorRenderer, RazorRenderer>();
        services.AddScoped<IPdfRenderer, PdfRenderer>();
        services.AddScoped<IStatusTransitionService, StatusTransitionService>();

        services.Configure<FileServiceSettings>(configuration.GetSection(nameof(FileServiceSettings)));
        services.Configure<VirusScanningSettings>(configuration);
        services.Configure<AwsS3Options>(configuration);
        services.Configure<AnalyticsSettings>(configuration);
        services.Configure<PanelListSettings>(configuration);

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

        var allFormats = FileFormatLocator.GetFormats(Assembly.GetExecutingAssembly(), true);
        services.AddSingleton<IFileFormatInspector>(new FileFormatInspector(allFormats));
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<IFireRiskAppraisalRepository, FireRiskAppraisalRepository>();
        services.AddScoped<IResponsibleEntityRepository, ResponsibleEntityRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IFireRiskWorksRepository, FireRiskWorksRepository>();
        services.AddScoped<IBuildingDetailsRepository, BuildingDetailsRepository>();
        services.AddScoped<IBuildingsInsuranceRepository, BuildingsInsuranceRepository>();
        services.AddScoped<IAlertRepository, AlertRepository>();
        services.AddScoped<IApplicationSchemeRepository, ApplicationSchemeRepository>();
        services.AddScoped<IPreTenderRepository, PreTenderRepository>();
        services.AddScoped<IProgressReportingRepository, ProgressReportingRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IWorkPackageRepository, WorkPackageRepository>();
        services.AddScoped<IScheduleOfWorksRepository, ScheduleOfWorksRepository>();
        services.AddScoped<ISupportTicketRepository, SupportTicketRepository>();
        services.AddScoped<IBankDetailsRepository, BankDetailsRepository>();
        services.AddScoped<IPaymentRequestRepository, PaymentRequestRepository>();
        services.AddScoped<IClosingReportRepository, ClosingReportRepository>();
        services.AddScoped<ISubContractorSurveyRepository, SubContractorSurveyRepository>();
        services.AddScoped<IVariationRequestRepository, VariationRequestRepository>();
        services.AddScoped<IDateRepository, DateRepository>();
        services.AddScoped<ISystemNotificationRepository, SystemNotificationRepository>();
        services.AddScoped<IMilestoneRepository, MilestoneRepository>();
        services.AddScoped<ICommunicationRepository, CommunicationRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<ILocalAuthorityCostCentreRepository, LocalAuthorityCostCentreRepository>();
        services.AddScoped<IGrantFundingRepository, GrantFundingRepository>();
        services.AddScoped<ILeaseHolderRepository, LeaseHolderRepository>();
        services.AddScoped<IRightToManageRepository, RightToManageRepository>();
        services.AddScoped<IThirdPartyCollaboratorRepository, ThirdPartyCollaboratorRepository>();
        services.AddScoped<IDataIngestionRepository, DataIngestionRepository>();
        services.AddScoped<IEvidenceOfThirdPartyContributionRepository, EvidenceOfThirdPartyContributionRepository>();
        services.AddScoped<IFireRiskAssessmentRepository, FireRiskAssessmentRepository>();
        services.AddScoped<IWorkPackageFireRiskAssessmentRepository, WorkPackageFireRiskAssessmentRepository>();
        services.AddScoped<IAlternateFundingRepository, AlternateFundingRepository>();
        services.AddScoped<IManageProgrammeRepository, ManageProgrammeRepository>();
    }
}
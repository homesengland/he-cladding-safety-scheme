using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Services.GovNotify;
using HE.Remediation.Core.Services.GovNotify.Models;
using HE.Remediation.Core.Services.GovNotify.ViewModels;
using HE.Remediation.Core.Services.PdfRendererService;

namespace HE.Remediation.Core.Services.Communication
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IBackgroundEmailCommunicationQueue _emailQueue;
        private readonly IApplicationDataProvider _applicationDataProvider; 
        private readonly IApplicationRepository _applicationRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IGovNotifyService _govNotifyService;
        private readonly IPdfRenderer _pdfRenderer;
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly ICommunicationRepository _communicationRepository;
        private readonly CommunicationConstants _communicationConstants;

        public CommunicationService(IBackgroundEmailCommunicationQueue emailQueue,
            IApplicationDataProvider applicationDataProvider,
            IApplicationRepository applicationRepository,
            IDateTimeProvider dateTimeProvider,
            IGovNotifyService govNotifyService,
            IPdfRenderer pdfRenderer,
            IFileService fileService,
            IFileRepository fileRepository,
            ICommunicationRepository communicationRepository,
            CommunicationConstants communicationConstants)
        {
            _emailQueue = emailQueue; 
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _dateTimeProvider = dateTimeProvider;
            _govNotifyService = govNotifyService;
            _pdfRenderer = pdfRenderer;
            _fileService = fileService;
            _fileRepository = fileRepository;
            _communicationRepository = communicationRepository;
            _communicationConstants = communicationConstants;
        }

        public async Task QueueEmailCommunication(EmailCommunicationRequest request)
        {
             await SendEmail(request);
        }

        private async Task SendEmail(EmailCommunicationRequest request)
        {
            var message = new SendEmailCommunicationTask(request);

            await _emailQueue.QueueBackgroundEmailCommunicationAsync(message);
        }

        private string GetEmailTypeRazorPageName(EEmailType emailType)
        {
            switch (emailType)
            {
                case EEmailType.ApplicationSubmitted:
                    return "ApplicationSubmittedEmail.cshtml";
                case EEmailType.SssfBuildingCompleteApplicationSubmitted:
                    return "SssfBuildingCompleteApplicationSubmitted.cshtml";
                case EEmailType.SssfBuildingNonCompleteApplicationSubmitted:
                    return "SssfBuildingNonCompleteApplicationSubmitted.cshtml";
                case EEmailType.WorksPackageSubmitted:
                    return "WorksPackageSubmittedEmail.cshtml";
                case EEmailType.ScheduleOfWorksSubmitted:
                    return "ScheduleOfWorksSubmittedEmail.cshtml";
                case EEmailType.PaymentRequestSubmitted:
                    return "PaymentRequestSubmittedEmail.cshtml";
                case EEmailType.VariationSubmitted:
                    return "VariationSubmittedEmail.cshtml";
                case EEmailType.ClosingReportSubmitted:
                    return "ClosingReportSubmittedEmail.cshtml";
                default:
                    throw new Exception("Email type not supported");
            }
        }

        public async Task<bool> InsertEmailCommunication(Guid applicationId, EEmailType emailType)
        {
            var applicationSummaryDetails = await _applicationRepository.GetApplicationSummaryDetails(applicationId);

            var govNotifyEmailResponse = await _govNotifyService.SendEmailAsync(
                new GovNotifyEmailRequestModel<ApplicationUpdateParameters>
                {
                    TemplateId = _communicationConstants.GetEmailTypeTemplateId(emailType),
                    EmailAddress = applicationSummaryDetails.PrimaryContactEmailAddress,
                    Personalisation = new ApplicationUpdateParameters
                    {
                        FirstName = applicationSummaryDetails.PrimaryContactFirstName,
                        Surname = applicationSummaryDetails.PrimaryContactSurname,
                        RecipientEmail = applicationSummaryDetails.PrimaryContactEmailAddress,
                        BuildingName = applicationSummaryDetails.BuildingName,
                        ReferenceNumber = applicationSummaryDetails.ReferenceNumber
                    }
                }
            );

            // Generate a PDF letter based on this buildings current communication stage
            var renderedFilePdf = await RenderCommunicationPdfTemplate(applicationSummaryDetails, emailType);

            // Save the file to S3 and insert the unorphaned DB record
            var savedFilePdfId = await SaveRenderedLetter(renderedFilePdf, _communicationConstants.GetEmailTypeName(emailType));

            await _communicationRepository.InsertCommunication(new InsertCommunicationParameters
            {
                AddedByUserId = null,
                ApplicationId = applicationId,
                EmailAddress = applicationSummaryDetails.PrimaryContactEmailAddress,
                Notes = _communicationConstants.GetEmailTypeName(emailType),
                CommunicationTypeId = (int)ECommunicationType.Email,
                CommunicationCategoryId = _communicationConstants.GetEmailTypeCategory(emailType),
                CommunicationDirectionId = (int)ECommunicationDirection.Outbound,
                CommunicationTriggerId = (int)ECommunicationTrigger.Automatic,
                CommunicationFileId = savedFilePdfId,
                DateAdded = _dateTimeProvider.UtcNow,
                DateSent = _dateTimeProvider.UtcNow,
                PhoneNumber = null,
                GovNotifyNotificationId = govNotifyEmailResponse.NotificationId,
                GovNotifyNotificationStatusId = (int)EGovNotifyNotificationStatusType.Email_Created,
                GovNotifyNotificationTypeId = (int)EGovNotifyNotificationType.Email
            });

            return true;
        }

        private Task<byte[]> RenderCommunicationPdfTemplate(GetApplicationSummaryDetailsResult applicationSummaryDetails, EEmailType emailType)
        {
            var viewPath = Path.Combine("Views", "GovNotify", GetEmailTypeRazorPageName(emailType));

            return _pdfRenderer.RenderGotNotifyLetterPdf(viewPath, MapApplicationSubmittedViewModel(applicationSummaryDetails));
        }

        private GovNotifyEmailViewModel MapApplicationSubmittedViewModel(GetApplicationSummaryDetailsResult applicationSummaryDetails)
            => new()
            {
                ContactFirstName = applicationSummaryDetails.PrimaryContactFirstName,
                ContactSurname = applicationSummaryDetails.PrimaryContactSurname,
                ContactEmailAddress = applicationSummaryDetails.PrimaryContactEmailAddress,
                ReferenceNumber = applicationSummaryDetails.ReferenceNumber,
                BuildingName = applicationSummaryDetails.BuildingName
            };

        private async Task<Guid> SaveRenderedLetter(byte[] renderedLetterPdf, string fileName)
        {
            await using var letterStream = new MemoryStream(renderedLetterPdf);
            var processPdfFileResult = await _fileService.ProcessPdfFile(letterStream, fileName);

            await _fileRepository.InsertFile(new InsertFileParameters
            {
                Id = processPdfFileResult.FileId,
                Extension = ".pdf",
                MimeType = "application/pdf",
                Size = renderedLetterPdf.Length,
                Name = $"{fileName}.pdf"
            });

            return processPdfFileResult.FileId;
        }
    }
}

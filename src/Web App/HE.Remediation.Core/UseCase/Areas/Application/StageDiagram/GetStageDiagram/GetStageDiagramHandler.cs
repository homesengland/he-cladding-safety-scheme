using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram
{
    public class GetStageDiagramHandler : IRequestHandler<GetStageDiagramRequest, GetStageDiagramResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;
        private readonly IProgressReportingRepository _progressReportingRepository;
        private readonly IMonthlyProgressReportingRepository _monthlyProgressReportingRepository;
        private readonly IScheduleOfWorksRepository _scheduleOfWorksRepository;
        private readonly IWorkPackageRepository _workPackageRepository;
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        private readonly IVariationRequestRepository _variationRequestRepository;
        private readonly IClosingReportRepository _closingReportRepository;
        private readonly IMilestoneRepository _milestoneRepository;

        public GetStageDiagramHandler(
            IApplicationDataProvider applicationDataProvider, 
            IDbConnectionWrapper db, 
            IProgressReportingRepository progressReportingRepository,
            IMonthlyProgressReportingRepository monthlyProgressReportingRepository,
            IScheduleOfWorksRepository scheduleOfWorksRepository, 
            IWorkPackageRepository workPackageRepository, 
            IPaymentRequestRepository paymentRequestRepository, 
            IVariationRequestRepository variationRequestRepository, 
            IClosingReportRepository closingReportRepository, 
            IMilestoneRepository milestoneRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
            _progressReportingRepository = progressReportingRepository;
            _monthlyProgressReportingRepository = monthlyProgressReportingRepository;
            _scheduleOfWorksRepository = scheduleOfWorksRepository;
            _workPackageRepository = workPackageRepository;
            _paymentRequestRepository = paymentRequestRepository;
            _variationRequestRepository = variationRequestRepository;
            _closingReportRepository = closingReportRepository;
            _milestoneRepository = milestoneRepository;
        }

        public async Task<GetStageDiagramResponse> Handle(GetStageDiagramRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var stageDiagramResponse = await _db.QuerySingleOrDefaultAsync<GetStageDiagramResponse>("GetStageDiagram", new
            {
                ApplicationId = applicationId
            });

            var submittedDate = await _db.QuerySingleOrDefaultAsync<DateTime?>("GetApplicationSubmittedDate", new
            {
                ApplicationId = applicationId
            });

            var isApplicationActive = await _db.QuerySingleOrDefaultAsync<bool>("CheckIsApplicationActive", new
            {
                ApplicationId = applicationId
            });


            var monthlyPaymentsOutstanding = await _paymentRequestRepository.GetMonthlyPaymentsOutstanding(applicationId);

            stageDiagramResponse.SubmittedDate = submittedDate;

            var monthlyProgressReports = await _monthlyProgressReportingRepository.GetProgressReports(applicationId);
            stageDiagramResponse.HasProgressReports = monthlyProgressReports.Count > 0;
            stageDiagramResponse.ProgressReports = monthlyProgressReports.OrderByDescending(x => x.DateCreated).Take(2).ToList();
            stageDiagramResponse.HasSubmittedProgressReports = monthlyProgressReports.Count > 2;

            var paymentRequests = await _paymentRequestRepository.GetPaymentRequests();
            if (paymentRequests != null && paymentRequests.Any())
            {
                stageDiagramResponse.PaymentRequests = paymentRequests.OrderByDescending(x => x.CreatedDate).Take(2).ToList();
            }

            var variationRequestApprovalStatus = await _variationRequestRepository.GetApprovalStatus();

            var closingReportAvailable = await _closingReportRepository.GetApplicationReadyForClosingReport(applicationId);
            var closingReportSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

            if (closingReportAvailable)
            {
                if (variationRequestApprovalStatus is null)
                {
                    closingReportAvailable = true;
                }
                else if ((variationRequestApprovalStatus != EVariationRequestApprovalStatus.Approved) &&
                         (variationRequestApprovalStatus != EVariationRequestApprovalStatus.Rejected))
                {
                    closingReportAvailable = false;
                }
            }

            var startedOnDate = await _milestoneRepository.GetStartOnSite(applicationId);
            var practicalCompletion = await _milestoneRepository.GetPracticalCompletion(applicationId);

            stageDiagramResponse.ShowClosingReport = closingReportAvailable;          
            stageDiagramResponse.HasWorkPackage = await _workPackageRepository.HasWorkPackage();
            stageDiagramResponse.IsWorkPackageSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();
            stageDiagramResponse.isWorkPackageConfirmedToProceed = await _workPackageRepository.GetWorkPackageConfirmToProceed();
            stageDiagramResponse.HasScheduleOfWorks = await _scheduleOfWorksRepository.HasScheduleOfWorks();
            stageDiagramResponse.IsScheduleOfWorksSubmitted = await _scheduleOfWorksRepository.IsScheduleOfWorksSubmitted();
            stageDiagramResponse.IsScheduleOfWorksApproved = await _scheduleOfWorksRepository.IsScheduleOfWorksApproved();
            stageDiagramResponse.HasSubmittedPaymentRequests = await _paymentRequestRepository.HasSubmittedPaymentRequests();
            stageDiagramResponse.HasInProgressVariationRequest = await _variationRequestRepository.HasInProgressVariationRequest();
            stageDiagramResponse.VariationRequestApprovalStatus = variationRequestApprovalStatus;
            stageDiagramResponse.IsVariationRequestSubmitted = await _variationRequestRepository.IsVariationRequestSubmitted();

            stageDiagramResponse.StartedOnSiteMilestoneDate = startedOnDate?.StartOnSiteDate;
            stageDiagramResponse.IsStartedOnSiteSubmitted = startedOnDate?.IsStartOnSiteSubmitted ?? false;
            stageDiagramResponse.PracticalCompletionMilestoneDate = practicalCompletion?.PracticalCompletionDate;
            stageDiagramResponse.IsPracticalCompletionSubmitted = practicalCompletion?.IsPracticalCompletionSubmitted ?? false;
            stageDiagramResponse.IsClosingReportSubmitted = closingReportSubmitted;

            stageDiagramResponse.ConsiderVariation = monthlyPaymentsOutstanding == 1;
            stageDiagramResponse.IsApplicationActive = isApplicationActive;

            return stageDiagramResponse ?? new GetStageDiagramResponse();
        }
    }
}

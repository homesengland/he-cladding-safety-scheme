using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.PaymentRequest;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.KeyDates;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories;

public interface IPaymentRequestRepository
{
    void SetPaymentRequestId(Guid? paymentRequestId);

    Task UpdatePaymentRequestTeamMemberActiveState(Guid teamMemberId, bool isActive);

    Task<List<GetTeamMembersResult>> GetPaymentRequestTeamMembers();

    Task<GetPaymentRequestEndVersionDatesResult> GetPaymentRequestEndVersionDates(Guid applicationId);
    
    Task<GetPaymentRequestResult> GetPaymentRequestDetails(Guid applicationId, Guid paymentRequestId);
    
    Task<GetPaymentRequestDeclarationResult> GetPaymentRequestDeclarationDetails(Guid paymentRequestId);

    Task<IReadOnlyCollection<PaymentRequestResult>> GetPaymentRequests();

    Task<List<PaymentCostReportResult>> GetCostReportsForPaymentRequest(Guid paymentRequestId);

    Task DeletePaymentRequestCostFile(Guid fileId, Guid paymentRequestId);

    Task InsertPaymentRequestCostFile(Guid fileId, Guid paymentRequestId);

    Task UpdatePaymentRequestDeclaration(Guid paymentRequestId, PaymentRequestDeclarationParameters declarationParameters);

    Task<KeyDatesResult> GetLatestWorkPackageKeyDates(Guid paymentRequestId, bool includeLinked = false);

    Task InsertWorkPackageKeyDetailsVersion(Guid paymentRequestId, KeyDatesParameters keyDates);

    Task<GetPaymentRequestProjectDetailsResult> GetPaymentRequestProjectDetails(Guid paymentRequestId);
    
    Task UpdatePaymentRequestUnsafeCladdingRemoved(Guid paymentRequestId, bool? unsafeCladdingRemoved);

    Task UpdatePaymentRequestUnsafeCladdingRemovedDate(Guid paymentRequestId, DateTime? unsafeCladdingRemovedDate);

    Task UpdatePaymentRequestUserCostChanged(Guid paymentRequestId,  PaymentRequestUserCostChangedParameters costParams);

    Task UpdatePaymentRequestCostChangedDetails(Guid paymentRequestId, PaymentRequestCostChangedParameters costParams);

    Task UpdatePaymentRequestThirdPartyContributionsChangedDetails(PaymentRequestThirdPartyContributionsChangedParameters thirdPartyContributionsParams);

    Task<bool> IsPaymentRequestExpired(Guid paymentRequestId);

    Task<bool> IsItALastScheduledPayment(Guid applicationId, Guid paymentRequestId);

    Task<bool> IsPaymentRequestSubmitted(Guid paymentRequestId);

    Task<bool> HasSubmittedPaymentRequests();

    Task SubmitPaymentRequest(Guid paymentRequestId, Guid? userId);

    Task UpdatePaymentRequestTaskStatus(Guid paymentRequestId, EPaymentRequestTaskStatus taskStatusId);

    Task UpdatePaymentRequestReasonForChange(Guid paymentRequestId, string reasonForChange);

    Task<IReadOnlyCollection<GetPaymentCostProfileResult>> GetCostsProfile();

    Task UpdatePaymentRequestAmount(Guid paymentRequestId, decimal amount);

    Task InsertScheduleOfWorksProfile(Guid paymentRequestId, List<UpdateCostParameters> costs);

    Task UpdateScheduleOfWorksCosts(Guid scheduleOfWorksCostProfileId, List<UpdateCostParameters> costs);

    Task UpdatePaymentRequestProjectDateChanged(Guid paymentRequestId, bool? projectDatesChanged);

    Task<bool> GetUnsafeCladdingAlreadyRemoved(Guid applicationId, Guid paymentRequestId);

    Task<Guid?> GetPaymentRequestFirstPaymentRequest(Guid applicationId);

    Task<OverviewResult> GetOverview(Guid paymentRequestId);

    Task<OverviewResult> GetOverview();
    
    Task<Guid?> GetSubcontractorSurveyId(Guid paymentRequestId);
    
    Task UpdateSubcontractorSurveyId(Guid paymentRequestId, Guid subContractorSurveyId);

    Task<int?> GetPaymentRequestVersion(Guid paymentRequestId);

    Task<int?> GetPaymentRequestProjectDuration(Guid paymentRequestId);
    Task<IReadOnlyCollection<GetPaymentRequestInvoiceFilesResult>> GetPaymentRequestInvoiceFiles(GetPaymentRequestInvoiceFilesParameters parameters);
    Task InsertPaymentRequestInvoiceFile(InsertPaymentRequestInvoiceFileParameters parameters);
    Task DeletePaymentRequestInvoiceFile(DeletePaymentRequestInvoiceFileParameters parameters);
    Task<int> GetMonthlyPaymentsOutstanding(Guid applicationId);
}

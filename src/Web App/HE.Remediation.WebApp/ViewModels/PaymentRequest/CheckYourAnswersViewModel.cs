using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCheckYourAnswers;
using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class CheckYourAnswersViewModel : PaymentRequestBaseViewModel
{
    public bool? CostsChanged { get; set; }

    public bool? ThirdPartyContributionsChanged { get; set; }
    public string PaymentRequestName { get; set; }    

    public string ReasonForChange { get; set; }

    public bool? UnsafeCladdingRemoved { get; set; }    
    public DateTime? UnsafeCladdingRemovedDate { get; set; }

    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }

    public decimal? ScheduledAmount { get; set; }

    public DateTime? LastCommunicationDate { get; set; }
    public List<PaymentRequestLastCommunicationFile> PaymentRequestLastCommunicationFiles { get; set; }
    public IList<string> PaymentRequestLastCommunicationFileNames { get; set; }

    public bool? ProjectDatesChanged { get; set; }

    public decimal? CurrentMonthCost { get; set; }

    public string AdditionalCostMonthTitle { get; set; }

    public decimal? AdditionalCostAmount { get; set; }

    public bool UnsafeCladdingAlreadyRemoved { get; set; }

    public List<PaymentRequestCostFile> PaymentRequestCostFiles { get; set; }
    public IList<string> PaymentRequestInvoiceFileNames { get; set; }
    public List<GetTeamMembersResult> TeamMembers { get; set; }

    public DateTime? ExpectedSubmissionDateForGateway3Application { get; set; }

    public DateTime? ExpectedPracticalCompletionDate { get; set; }
}

public class PaymentRequestCostFile
{
    public string Name { get; set; }
}

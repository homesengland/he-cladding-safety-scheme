using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectDates;

public class GetProjectDatesResponse
{
    public bool? ProjectDatesChanged { get; set; }
    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
    public bool IsFirstPaymentRequest { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public bool IsSubmitted { get; set; }

    public bool IsExpired { get; set; }
}

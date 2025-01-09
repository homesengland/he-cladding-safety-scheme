using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class RemoveMemberViewModel : PaymentRequestBaseViewModel
{
    public Guid TeamMemberId { get; set; }

    public string TeamMemberName { get; set; }

    public bool? Confirm { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}

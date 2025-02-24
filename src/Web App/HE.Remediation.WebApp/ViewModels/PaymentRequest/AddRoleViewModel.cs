using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class AddRoleViewModel : PaymentRequestBaseViewModel
{
    public ETeamRole? TeamRole { get; set; }

    public List<ETeamRole> AvailableTeamRoles { get; set; }

    public int Version { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}

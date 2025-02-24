using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.PaymentRequest.Shared;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ProjectTeamViewModel : PaymentRequestBaseViewModel
{
    public bool? CostsChanged { get; set; }
    public bool EndDateSlipped { get; set; }
    public bool? ThirdPartyContributionsChanged { get; set; }
    public List<GetTeamMembersResult> TeamMembers { get; set; }
    public List<ETeamRole> MissingRoles { get; set; }
}

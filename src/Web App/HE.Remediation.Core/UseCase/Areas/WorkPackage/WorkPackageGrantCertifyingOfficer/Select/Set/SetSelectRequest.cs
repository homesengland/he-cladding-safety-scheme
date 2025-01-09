using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Select.Set;

public class SetSelectRequest : IRequest
{
    public Guid? SelectedProjectTeamMemberId { get; set; }    
}

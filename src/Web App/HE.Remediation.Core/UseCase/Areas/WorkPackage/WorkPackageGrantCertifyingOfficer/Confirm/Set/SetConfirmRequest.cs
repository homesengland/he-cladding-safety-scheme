using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Confirm.Set;

public class SetConfirmRequest : IRequest
{
    public ECertifyingOfficerResponse CertifyingOfficerResponse { get; set; }

    public ETeamRole? RoleId { get; set; }
}

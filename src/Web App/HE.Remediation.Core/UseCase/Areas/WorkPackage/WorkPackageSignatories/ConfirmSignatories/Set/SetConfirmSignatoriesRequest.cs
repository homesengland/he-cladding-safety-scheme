
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageSignatories.ConfirmSignatories.Set;

public class SetConfirmSignatoriesRequest : IRequest
{
    public bool? AreSignatoriesCorrect { get; set; }
}

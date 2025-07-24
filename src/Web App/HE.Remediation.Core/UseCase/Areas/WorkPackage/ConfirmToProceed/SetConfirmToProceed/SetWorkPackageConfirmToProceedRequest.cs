using HE.Remediation.Core.Enums;

using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.SetConfirmToProceed
{
    public class SetWorkPackageConfirmToProceedRequest : IRequest<Unit>
    {
        public ENoYes? IsConfirmedToProceed { get; set; }
    }
}

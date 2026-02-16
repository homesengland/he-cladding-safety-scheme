using HE.Remediation.Core.Enums;

using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.SetConfirmToProceed
{
    public class SetWorkPackageConfirmToProceedRequest : IRequest<Unit>
    {
        public ENoYes? IsConfirmedToProceed { get; set; }
    }
}

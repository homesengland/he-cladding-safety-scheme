
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.GetConfirmToProceed
{
    public class GetWorkPackageConfirmToProceedResponse
    {
        public string ApplicationReferenceNumber { get; set; }

        public string BuildingName { get; set; }

        public ENoYes? IsConfirmedToProceed { get; set; }
    }
}

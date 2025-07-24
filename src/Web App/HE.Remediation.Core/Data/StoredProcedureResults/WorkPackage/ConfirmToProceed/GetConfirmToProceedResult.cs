
namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ConfirmToProceed
{
    public class GetConfirmToProceedResult
    {
        public string ApplicationReferenceNumber { get; set; }

        public string BuildingName { get; set; }

        public bool IsConfirmedToProceed { get; set; }
    }
}

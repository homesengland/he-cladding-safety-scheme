
namespace HE.Remediation.Core.Data.StoredProcedureParameters.BuildingDetails
{
    public class UpdateConstructionCompletionDateParameters
    {
        public Guid ApplicationId { get; set; }

        public DateTime? ConstructionCompletionDate { get; set; }
    }
}

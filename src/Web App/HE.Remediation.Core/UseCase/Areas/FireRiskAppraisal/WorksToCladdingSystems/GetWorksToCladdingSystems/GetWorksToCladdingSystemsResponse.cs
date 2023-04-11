namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetWorksToCladdingSystems
{
    public class GetWorksToCladdingSystemsResponse
    {
        public Guid FireRiskCladdingSystemsId { get; set; }
        public string InsulationTypeName { get; set; }
        public string CladdingSystemTypeName { get; set; }
    }
}

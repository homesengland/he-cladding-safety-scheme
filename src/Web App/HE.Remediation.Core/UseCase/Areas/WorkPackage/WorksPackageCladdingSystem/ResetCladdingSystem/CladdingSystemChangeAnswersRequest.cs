namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorksPackageCladdingSystem.ResetCladdingSystem
{
    public class CladdingSystemChangeAnswersRequest
    {
        public Guid FireRiskCladdingSystemsId { get; set; }

        public int CladdingSystemIndex { get; set; }
    }
}

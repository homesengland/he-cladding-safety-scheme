namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class CladdingSystemViewModel
    {
        public Guid? FireRiskCladdingSystemsId { get; set; }
        public int? CladdingSystemTypeId { get; set; }
        public int? InsulationTypeId { get; set; }
        public IEnumerable<CladdingTypeViewModel> CladdingTypes { get; set; }
        public IEnumerable<InsulationTypeViewModel> InsulationTypes { get; set; }
    }
}

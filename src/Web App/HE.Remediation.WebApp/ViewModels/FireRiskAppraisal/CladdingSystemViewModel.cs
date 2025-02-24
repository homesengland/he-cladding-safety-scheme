namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class CladdingSystemViewModel
    {
        public Guid? FireRiskCladdingSystemsId { get; set; }
        public int? CladdingSystemTypeId { get; set; }
        public int? InsulationTypeId { get; set; }
        public int? CladdingManufacturerId { get; set; }
        public int? InsulationManufacturerId { get; set; }
        public string OtherInsulationManufacturer { get; set; }
        public string OtherCladdingManufacturer { get; set; }
        public string OtherInsulationType { get; set; }
        public string OtherCladdingType { get; set; }
        public IEnumerable<CladdingTypeViewModel> CladdingTypes { get; set; }
        public IEnumerable<InsulationTypeViewModel> InsulationTypes { get; set; }
        public IEnumerable<CladdingManufacturerViewModel> CladdingManufacturers { get; set; }
        public IEnumerable<CladdingManufacturerViewModel> InsulationManufacturers { get; set; }
    }
}

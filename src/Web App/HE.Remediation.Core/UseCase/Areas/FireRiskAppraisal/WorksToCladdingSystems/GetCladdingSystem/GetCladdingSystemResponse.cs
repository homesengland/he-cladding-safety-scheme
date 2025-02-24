using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetCladdingSystem
{
    public class GetCladdingSystemResponse
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
        public IEnumerable<GetCladdingManufacturerResult> CladdingManufacturers { get; set; }
        public IEnumerable<GetCladdingManufacturerResult> InsulationManufacturers { get; set; }
        public IEnumerable<GetCladdingTypeResult> CladdingTypes { get; set; }
        public IEnumerable<GetInsulationTypeResult> InsulationTypes { get; set; }
    }
}
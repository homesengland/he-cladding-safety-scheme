﻿namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetWorksToCladdingSystems
{
    public class GetWorksToCladdingSystemsResponse
    {
        public Guid FireRiskCladdingSystemsId { get; set; }
        public string InsulationTypeName { get; set; }
        public string CladdingSystemTypeName { get; set; }
        public string CladdingManufacturerName { get; set; }
        public string InsulationManufacturerName { get; set; }
        public string OtherCladdingManufacturer { get; set; }
        public string OtherInsulationManufacturer { get; set; }
        public string OtherCladdingType { get; set; }
        public string OtherInsulationType { get; set; }
    }
}

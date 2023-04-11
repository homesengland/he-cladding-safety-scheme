﻿using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetCladdingSystem
{
    public class GetCladdingSystemResponse
    {
        public Guid? FireRiskCladdingSystemsId { get; set; }
        public int? CladdingSystemTypeId { get; set; }
        public int? InsulationTypeId { get; set; }
        public IEnumerable<GetCladdingTypeResult> CladdingTypes { get; set; }
        public IEnumerable<GetInsulationTypeResult> InsulationTypes { get; set; }
    }
}
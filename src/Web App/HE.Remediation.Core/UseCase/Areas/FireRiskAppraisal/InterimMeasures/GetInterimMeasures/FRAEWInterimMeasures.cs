namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InterimMeasures.GetInterimMeasures
{
    public class FRAEWInterimMeasures
    {
        public int BuildingInterimMeasuresId { get; set; }
        public string BuildingInterimMeasuresText { get; set; }
        public int EvacuationStrategyTypeId { get; set; }
        public string EvacuationStrategyText { get; set; }
        public int NumberOfStairwellsPrompt { get; set; }
        public int? NumberOfStairwells { get; set; }
        public int ExternalWallAndBalconiesPolicyId { get; set; }
        public int FireAndResueAccessRestrictionsId { get; set; }
        public string FireAndResueAccessRestrictions { get; set; }
    }
}
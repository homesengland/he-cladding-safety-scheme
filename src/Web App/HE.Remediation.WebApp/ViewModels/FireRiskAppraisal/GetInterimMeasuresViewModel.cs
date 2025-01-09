using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class GetInterimMeasuresViewModel
    {
        public EYesNoNonBoolean? BuildingInterimMeasures { get; set; }
        public string BuildingInterimMeasuresText { get; set; }
        public EEvacuationStrategy? EvacuationStrategyType { get; set; }
        public string EvacuationStrategyText { get; set; }
        public ENoYes? NumberOfStairwellsPrompt { get; set; }
        public int? NumberOfStairwells { get; set; }
        public EYesNoNonBoolean? ExternalWallAndBalconiesPolicy { get; set; }
        public EYesNoNonBoolean? FireAndResueAccessRestrictions { get; set; }
        public string FireAndResueAccessRestrictionsText { get; set; }

        public IEnumerable<EInterimMeasuresType> BuildingInterimMeasuresTypes { get; set; } =
           new List<EInterimMeasuresType>();
        public string ReturnUrl { get; set; }
    }
}

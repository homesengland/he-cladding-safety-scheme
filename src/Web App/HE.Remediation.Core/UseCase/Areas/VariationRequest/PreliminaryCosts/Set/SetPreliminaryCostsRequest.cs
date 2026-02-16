using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.PreliminaryCosts.Set
{
    public class SetPreliminaryCostsRequest : IRequest
    {
        public decimal? VariationMainContractorPreliminariesAmount { get; set; }
        public string VariationMainContractorPreliminariesDescription { get; set; }
        public decimal? VariationAccessAmount { get; set; }
        public string VariationAccessDescription { get; set; }
        public decimal? VariationOverheadsAndProfitAmount { get; set; }
        public string VariationOverheadsAndProfitDescription { get; set; }
        public decimal? VariationContractorContingenciesAmount { get; set; }
        public string VariationContractorContingenciesDescription { get; set; }
    }
}

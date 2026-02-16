using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubContractorRatings;

public class SetSubContractorRatingsRequest : IRequest
{
    public SubContractorRating Ratings { get; set; }
    public bool Complete { get; set; }
}

public class SubContractorRating
{
    public Guid Id { get; set; }
    public int QualityOfWork { get; set; }
    public int ValueForMoney { get; set; }
    public int Reliability { get; set; }
    public int ConsiderationOfResidents { get; set; }
    public int OverallSatisfaction { get; set; }
}

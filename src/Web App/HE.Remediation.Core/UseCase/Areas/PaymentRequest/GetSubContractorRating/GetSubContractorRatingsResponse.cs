using HE.Remediation.Core.Data.StoredProcedureResults.SubContractorRatings;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractorRating;

public class GetSubContractorRatingsResponse
{
    public GetSubContractorRatingResult Ratings { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }
}
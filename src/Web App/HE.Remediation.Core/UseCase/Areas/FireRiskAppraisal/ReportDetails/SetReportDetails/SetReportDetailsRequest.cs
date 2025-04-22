using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.SetReportDetails;

public class SetReportDetailsRequest: IRequest<Unit>
{
    public string AuthorsName { get; set; }
    public string PeerReviewPerson { get; set; }
    public decimal? FraewCost { get; set; }
    public string CompanyUndertakingReport { get; set; }
    public int? NumberOfStoreys { get; set; }    

    public decimal? BuildingHeight { get; set; }

    public EBasicComplexType BasicComplexId { get; set; }
}

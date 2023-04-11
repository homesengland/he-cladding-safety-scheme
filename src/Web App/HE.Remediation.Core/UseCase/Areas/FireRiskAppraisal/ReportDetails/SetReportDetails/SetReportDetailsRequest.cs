using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.SetReportDetails;

public class SetReportDetailsRequest: IRequest<Unit>
{
    public string AuthorsName { get; set; }
    public string PeerReviewPerson { get; set; }
    public string UndertakingFirm { get; set; }
    public int? NumberOfStoreys { get; set; }    

    public int? BuildingHeight { get; set; }

    public ENoYes? BuildingInterimMeasures { get; set; }

    public EBasicComplexType BasicComplexId { get; set; }
}

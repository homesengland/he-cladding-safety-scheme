using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.StoredProcedureParameters.SubContractorRatings;

public class UpdateSubcontractorRatingParameters
{
    public int QualityOfWork { get; set; }
    public int ValueForMoney { get; set; }
    public int Reliability { get; set; }
    public int ConsiderationOfResidents { get; set; }
    public int OverallSatisfaction { get; set; }
    public ETaskStatus Status { get; set; }
}
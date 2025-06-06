﻿using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.GetReportDetails;

public class FRAEWReportDetails
{
    public string AuthorsName { get; set; }
    public string PeerReviewPerson { get; set; }
    public decimal? FraewCost { get; set; }
    public string UndertakingFirm { get; set; }
    public int? NumberOfStoreys { get; set; }
    public decimal? BuildingHeight { get; set; }
    public ENoYes? BuildingInterimMeasures { get; set; }
    public EBasicComplexType? BasicComplexId { get; set; }
}

﻿
namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.GetRecommendedWorks;

public class FRAEWBuildingDetails
{
    public string BuildingAddress { get; set; }

    public DateTime? FRAEWInstructedDate { get; set; }

    public string BuildingName { get; set; }

    public DateTime? FRAEWCompletedDate { get; set; }

    public string CompanyUndertakingReport { get; set; }
}

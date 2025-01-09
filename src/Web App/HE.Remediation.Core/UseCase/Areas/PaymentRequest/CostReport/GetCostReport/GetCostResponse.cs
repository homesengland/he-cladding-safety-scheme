﻿using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.CostReport.GetCostReport;

public class GetCostResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public FileResult File { get; set; }
}

﻿using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.PursuingThirdPartyContribution.Get;

public class GetPursuingThirdPartyContributionResponse
{
    public EThirdPartyContributionPursuitStatus? ThirdPartyContributionPursuitStatusTypeId { get; set; }
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public bool IsSubmitted { get; set; }

}

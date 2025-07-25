﻿using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.PursuingThirdPartyContribution.Set;

public class SetPursuingThirdPartyContributionRequest : IRequest
{
    public EThirdPartyContributionPursuitStatus? ThirdPartyContributionPursuitStatusTypeId { get; set; }
}

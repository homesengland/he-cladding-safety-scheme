﻿using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystem.Get;

public class GetCladdingSystemRequest : IRequest<GetCladdingSystemResponse>
{
    public Guid FireRiskCladdingSystemsId { get; set; }

    public int CladdingSystemIndex { get; set; }
}

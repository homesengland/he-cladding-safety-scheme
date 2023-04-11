﻿using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ResidentialUnits.SetResidentialUnits
{
    public class SetResidentialUnitsRequest : IRequest<Unit>
    {
        public int? ResidentialUnitsCount { get; set; }
        public ENoYes? NonResidentialUnits { get; set; }
    }
}
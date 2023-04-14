﻿using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanySubType.SetResponsibleEntityCompanySubType;

public class SetResponsibleEntityCompanySubTypeRequest : IRequest<Unit>
{
    public EApplicationResponsibleEntityOrganisationSubType? OrganisationSubType { get; set; }
    public string OrganisationSubTypeDescription { get; set; }
}

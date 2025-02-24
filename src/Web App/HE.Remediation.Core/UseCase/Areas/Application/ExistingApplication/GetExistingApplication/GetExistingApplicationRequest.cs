﻿using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication
{
    public class GetExistingApplicationRequest : IRequest<IReadOnlyCollection<GetExistingApplicationResponse>>
    {
        public string Search { get; set; }
    }
}

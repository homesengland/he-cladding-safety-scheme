﻿using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Submit.Set;

public class SetSubmitRequest : IRequest
{
    private SetSubmitRequest()
    {
    }

    public static readonly SetSubmitRequest Request = new();
}

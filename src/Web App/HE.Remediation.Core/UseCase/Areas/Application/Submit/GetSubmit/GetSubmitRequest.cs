﻿using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.Submit.GetSubmit
{
    public class GetSubmitRequest : IRequest<GetSubmitResponse>
    {
        private GetSubmitRequest()
        {

        }

        public static readonly GetSubmitRequest Request = new();
    }
}
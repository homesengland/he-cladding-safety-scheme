﻿using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.CheckYourAnswers.SetCheckYourAnswers
{
    public class SetCheckYourAnswersRequest : IRequest<Unit>
    {
        private SetCheckYourAnswersRequest()
        {
        }

        public static readonly SetCheckYourAnswersRequest Request = new();
    }
}

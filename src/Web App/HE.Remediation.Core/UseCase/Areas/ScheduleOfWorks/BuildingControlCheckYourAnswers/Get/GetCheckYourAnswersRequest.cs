using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingControlCheckYourAnswers.Get;

public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
{
    private GetCheckYourAnswersRequest()
    {
    }

    public static GetCheckYourAnswersRequest Request => new();
}
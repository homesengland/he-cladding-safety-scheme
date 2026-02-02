using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.DeveloperPledgeStop.SetDeveloperPledgeStop
{
    public class SetDeveloperPledgeStopRequest : IRequest<Unit>
    {
        private SetDeveloperPledgeStopRequest()
        {
        }

        public static readonly SetDeveloperPledgeStopRequest Request = new();
    }
}

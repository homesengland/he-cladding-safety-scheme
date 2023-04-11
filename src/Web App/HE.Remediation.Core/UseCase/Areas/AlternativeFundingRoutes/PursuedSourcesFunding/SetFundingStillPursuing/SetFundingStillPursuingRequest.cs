using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetFundingStillPursuing
{
    public class SetFundingStillPursuingRequest : IRequest<Unit>
    {
        public IEnumerable<EFundingStillPursuing> FundingStillPursuing { get; set; }
    }
}

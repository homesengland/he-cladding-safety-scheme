using HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Get;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Set
{
    public class SetAdjustEndDateRequest : IRequest
    {
        public int? NewEndMonth { get; set; }
        public int? NewEndYear { get; set; }
    }
}
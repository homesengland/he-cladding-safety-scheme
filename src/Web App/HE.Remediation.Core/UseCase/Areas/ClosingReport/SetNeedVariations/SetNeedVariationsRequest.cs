using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetNeedVariations
{
    public class SetNeedVariationsRequest : IRequest
    {
        public bool? NeedVariations { get; set; }
    }
}
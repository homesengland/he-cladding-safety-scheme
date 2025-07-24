using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.CostSchedulePdf
{
    public class CostSchedulePdfRequest : IRequest<byte[]>
    {
        private CostSchedulePdfRequest()
        {
        }

        public static readonly CostSchedulePdfRequest Request = new();
    }
}

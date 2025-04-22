using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetNeedVariations
{
    public class GetNeedVariationsRequest : IRequest<GetNeedVariationsReponse>
    {
        private GetNeedVariationsRequest()
        {
        }

        public static readonly GetNeedVariationsRequest Request = new();
    }
}
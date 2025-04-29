using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetNeedVariations
{
    public class GetNeedVariationsReponse
    {
        public bool? NeedVariations { get; set; }

        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public bool IsSubmitted { get; set; }
    }
}

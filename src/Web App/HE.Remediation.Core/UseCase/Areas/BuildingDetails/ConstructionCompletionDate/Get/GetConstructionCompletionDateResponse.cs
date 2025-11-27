using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConstructionCompletionDate.Get
{
    public class GetConstructionCompletionDateResponse
    {
        public int? ConstructionCompletionDateMonth { get; set; }
        public int? ConstructionCompletionDateYear { get; set; }
    }
}

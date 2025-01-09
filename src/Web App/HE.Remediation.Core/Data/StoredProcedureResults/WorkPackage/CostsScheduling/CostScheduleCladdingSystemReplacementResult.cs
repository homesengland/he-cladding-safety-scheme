using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling
{
    public class CostScheduleCladdingSystemReplacementResult
    {
        public bool IsBeingRemoved { get; set; }
        public string CladdingType { get; set; }
        public string CladdingManufacturer { get; set; }
        public string InsulationMaterial { get; set; }
        public string InsulationManufacturer { get; set; }
    }
}

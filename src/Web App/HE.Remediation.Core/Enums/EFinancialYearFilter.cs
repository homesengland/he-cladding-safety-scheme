using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.Core.Enums
{
    public enum EFinancialYearFilter
    {
        [Display(Name = "FY' 2024/25")]
        FY24_25 = 1,

        [Display(Name = "FY' 2025/26")]
        FY25_26 = 2,

        [Display(Name = "FY' 2026/27")]
        FY26_27 = 3,

        [Display(Name = "FY' 2027/28")]
        FY27_28 = 4,

        [Display(Name = "FY' 2028/29 and later")]
        FY28_29 = 5,

        [Display(Name = "No data")]
        None = 6
    }
}

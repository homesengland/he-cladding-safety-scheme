using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.Core.Enums
{
    public enum EThirdPartyContributionStatusOfAttempt
    {
        [Display(Name="In Progress")]
        InProgress = 1,
        [Display(Name = "Not Yet Started")]
        NotYetStarted = 2,
        [Display(Name = "Success")]
        Success = 3,
        [Display(Name = "Fail")]
        Fail = 4
    }
}

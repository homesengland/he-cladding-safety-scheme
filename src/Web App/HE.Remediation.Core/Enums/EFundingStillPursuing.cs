using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.Core.Enums
{
    public enum EFundingStillPursuing
    {
        [Display(Name = "Developer that has signed up to the Developer's pledge")]
        SignedUpDevelopersPledge = 1,
        [Display(Name = "Developer that has not signed up to the pledge, or freeholder")]
        NotSignedUpDevelopersPledge,
        [Display(Name = "Warranty, insurance or other claim against contractor or design team")]
        ClaimAgainstContracter,
        [Display(Name = "Other")]
        Other
    }
}

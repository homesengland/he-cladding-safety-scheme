using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.Core.Enums
{
    public enum EApplicationRole
    {
        [Display(Name = "Admin")]
        OrganisationAdmin = 1,
        [Display(Name = "Team member")]
        OrganisationUser = 2,
        [Display(Name = "Contributer")]
        Contributer = 3
    }
}

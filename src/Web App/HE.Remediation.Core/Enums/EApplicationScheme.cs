using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.Core.Enums
{
    public enum EApplicationScheme
    {
        [Display(Name = "CSS")]
        CladdingSafetyScheme = 1, // Cladding Safety Scheme
        [Display(Name = "RAS")]
        ResponsibleActorsScheme = 2, // Responsible Actors Scheme (RAS)
        [Display(Name = "SSSF")]
        SocialSector = 3, // Social Sector self-funded
        [Display(Name = "PSSF")]
        SelfRemediating = 4 // Private Sector self-funded (not RAS)
    }
}

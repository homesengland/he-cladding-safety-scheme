using System.ComponentModel.DataAnnotations;

namespace HE.Remediation.Core.Enums
{
    public enum EApplicationScheme
    {
        [Display(Name = "CSS", Description = "Cladding safety scheme")]
        CladdingSafetyScheme = 1, 
        [Display(Name = "RAS", Description = "Responsible actors scheme")]
        ResponsibleActorsScheme = 2, 
        [Display(Name = "SSSF", Description = "Social sector")]
        SocialSector = 3, 
        [Display(Name = "PSSF", Description = "Private sector")]
        SelfRemediating = 4
    }
}

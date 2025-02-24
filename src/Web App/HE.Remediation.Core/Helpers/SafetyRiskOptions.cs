using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Helpers
{
    public static class SafetyRiskOptions
    {
        const string CompleteCladdingReplacement = "Complete removal and replacement of the unique cladding system or systems";
        const string PartialCladdingReplacement = "Partial removal and replacement of the unique cladding system or systems";
        const string EncapsulationOfCombustibles = "Encapsulation of the combustible materials present in the cladding system";
        const string FillingCavaties = "Filling cavaties with non-combustible insulation";
        const string WorksToFireBarriers = "Works to Fire Barriers";
        const string InstallationOfFireAndSmokeAlarms = "Installation or upgrades to fire alarms and or smoke detection";
        const string WorksToImproveAccess = "Works to improve access and facilities for the fire service";
        const string WorksToMeansOfEscape = "Works to means of escape";
        const string WorksToSmokeExtraction = "Works to smoke extraction";
        const string WorksToInternalCompartmentation = "Works to internal compartmentation";
        const string DontKnow = "Don't know";
        const string Other = "Other";

        public static string GetOptionQuestion(ERiskSafetyMitigationType type)
        {
            switch (type)
            {
                case ERiskSafetyMitigationType.CompleteCladdingReplacement :
                    return CompleteCladdingReplacement;
                case ERiskSafetyMitigationType.PartialCladdingReplacement:
                    return PartialCladdingReplacement;
                case ERiskSafetyMitigationType.EncapsulationOfCombustibles:
                    return EncapsulationOfCombustibles;
                case ERiskSafetyMitigationType.FillingCavaties:
                    return FillingCavaties;
                case ERiskSafetyMitigationType.WorksToFireBarriers:
                    return WorksToFireBarriers;
                case ERiskSafetyMitigationType.InstallationOfFireAndSmokeAlarms:
                    return InstallationOfFireAndSmokeAlarms;
                case ERiskSafetyMitigationType.WorksToImproveAccess:
                    return WorksToImproveAccess;
                case ERiskSafetyMitigationType.WorksToMeansOfEscape:
                    return WorksToMeansOfEscape;
                case ERiskSafetyMitigationType.WorksToSmokeExtraction:
                    return WorksToSmokeExtraction;
                case ERiskSafetyMitigationType.WorksToInternalCompartmentation:
                    return WorksToInternalCompartmentation;      
                case ERiskSafetyMitigationType.DontKnow:
                    return DontKnow;
                case ERiskSafetyMitigationType.Other:
                    return Other;
                default: return "Unknown";
            }
        }
    }
}

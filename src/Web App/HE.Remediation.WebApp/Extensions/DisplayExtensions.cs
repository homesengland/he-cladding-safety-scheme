using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.Extensions
{
    public static class DisplayExtensions
    {
        public static string ForDisplay(this DateTime? nullableDateTime)
        {
            return nullableDateTime == null ? "-" : nullableDateTime.Value.ToString("MM/yyyy");
        }

        public static string ForDisplay(this string nullableString)
        {
            return string.IsNullOrEmpty(nullableString) ? "-" : nullableString;
        }

        public static string ForDisplay(this bool? nullableBoolean)
        {
            return nullableBoolean == null ? "-" : nullableBoolean.Value ? "Yes" : "No";
        }

        public static string ForDisplay(this EYesNoNonBoolean? yesNoNonBoolean)
        {
            return yesNoNonBoolean switch
            {
                EYesNoNonBoolean.Yes => "Yes",
                EYesNoNonBoolean.No => "No",
                EYesNoNonBoolean.DontKnow => "Don't Know",
                _ => "-"
            };
        }

        public static string ForDisplay(this EProgressReportKeyDatesChangeType? nullableKeyDatesChangeType)
        {
            return nullableKeyDatesChangeType switch
            {
                EProgressReportKeyDatesChangeType.SubContractorsNotInContract => "Sub-contractors not in contract",
                EProgressReportKeyDatesChangeType.RegulatorySignOff => "Regulatory sign off",
                EProgressReportKeyDatesChangeType.PlanningDelay => "Planning delay",
                EProgressReportKeyDatesChangeType.ResponsibleEntityIssues => "Responsible entity issues",
                EProgressReportKeyDatesChangeType.LegalIssues => "Legal issues",
                EProgressReportKeyDatesChangeType.LaOrFRSDelay => "LA or F&RS delay",
                EProgressReportKeyDatesChangeType.Other => "Other",
                _ => "-"
            };
        }

        public static string ForDisplay(this EContractorTenderType? nullablEContractorTenderType)
        {
            return nullablEContractorTenderType switch
            {
                EContractorTenderType.Competitive => "Competitive tender",
                EContractorTenderType.NonCompetitive => "Non-competitive tender",
                EContractorTenderType.UsingOriginalContractor => "Using the original contractor",
                _ => "-"
            };
        }
    }
}

using HE.Remediation.Core.Enums;
using System.Text.RegularExpressions;

namespace HE.Remediation.Core.UseCase.DataIngest.RAS;

public class ImportedDataRow(Dictionary<string, string> importData)
{
    private readonly Dictionary<string, string> _importData = importData;

    // Building Details

    public string Developer
    {
        get
        {
            return _importData.GetSanitizedValue("Developer");
        }
    }

    public string BuildingName { 
        get { 
            var blankIfInvalidLength = 150;
            var text = _importData.GetSanitizedValue("Building_Name");
            if (text?.Length > blankIfInvalidLength) return null;
            return text;
        } 
    }

    public string AddressLine1 { 
        get { 
            var blankIfInvalidLength = 150;
            var text = _importData.GetSanitizedValue("Address_Line_1");
            if (text?.Length > blankIfInvalidLength) return null;
            return text;
        } 
    }

    public string PostCode
    {
        get
        {
            var blankIfInvalidLength = 10;
            var text = _importData.GetSanitizedValue("Postcode");
            if (text?.Length > blankIfInvalidLength) return null;
            return text;
        }
    }

    public string TownCity { 
        get { 
            var blankIfInvalidLength = 150;
            var text = _importData.GetSanitizedValue("Town_City");
            if (text?.Length > blankIfInvalidLength) return null;
            return text;
        } 
    }

    public string LocalAuthority { 
        get {
            var blankIfInvalidLength = 150;
            var text = _importData.GetSanitizedValue("Local_Authority");
            if (text?.Length > blankIfInvalidLength) return null;
            return text;
        } 
    }

    public string DevelopmentName
    {
        get
        {
            return _importData.GetSanitizedValue("Development_Name");
        }
    }

    public decimal? BuildingHeightInMetres
    {
        get
        {
            var number = _importData.GetSanitizedValue("Building_Height_Metres");
            if (!decimal.TryParse(Regex.Replace(number ?? "", @"[^\d]", ""), out decimal count))
            {
                return null;
            }
            return count;
        }
    }

    public int? BuildingHeightInStoreys
    {
        get
        {
            var number = _importData.GetSanitizedValue("Building_Height_Storeys");
            if (!int.TryParse(Regex.Replace(number ?? "", @"[^\d]", ""), out int count))
            {
                return null;
            }
            return count;
        }
    }

    public string PracticalCompletionDate // Not needed?
    {
        get
        {
            return _importData.GetSanitizedValue("Practical_Completion_Date");
        }
    }

    public string MajorRefurbishmentDate // Not needed?
    {
        get
        {
            return _importData.GetSanitizedValue("Major_Refurbishment_Date");
        }
    }

    public int? DwellingUnitsTotal
    {
        get {
            var residentialUnits = _importData.GetSanitizedValue("Dwelling_Units_Responsible_For");
            if (!int.TryParse(Regex.Replace(residentialUnits ?? "", @"[^\d]", ""), out int count))
            {
                return null;
            }

            return count;
        }
    }

    public int? LeaseholdUnits // Not needed?
    {
        get
        {
            var number = _importData.GetSanitizedValue("Leasehold_Units");
            if (!int.TryParse(Regex.Replace(number ?? "", @"[^\d]", ""), out int count))
            {
                return null;
            }

            return count;
        }
    }

    public int? NonResidentialUnits
    {
        get
        {
            var number = _importData.GetSanitizedValue("Non_Residential_Units");
            if (!int.TryParse(Regex.Replace(number ?? "", @"[^\d]", ""), out int count))
            {
                return null;
            }

            return count;
        }
    }

    public string LowCostRentalHomesRegisteredProvider // Not needed?
    {
        get
        {
            return _importData.GetSanitizedValue("Low_Cost_Rental_Homes_Registered_Provider");
        }
    }

    public string CurrentFreeholder // Not needed?
    {
        get
        {
            return _importData.GetSanitizedValue("Current_Freeholder");
        }
    }

    public EResponsibleEntityRelation? ResponsibleEntityOrganisationType
    {
        get
        {
            return _importData.GetSanitizedValue("RE_Organisation_Type", true).ToResponsibleEntityRelation();
        }
    }

    public string ResponsibleEntityName
    {
        get
        {
            return _importData.GetSanitizedValue("Responsible_Entity_Name");
        }
    }

    // FRAEW

    public bool HasFRA
    {
        get
        {
            var answer = _importData.GetSanitizedValue("Has_FRA", true);
            if(string.IsNullOrEmpty(answer))
            {
                return false;
            }
            return answer.StartsWith("y", StringComparison.CurrentCultureIgnoreCase);
        }
    }

    public string FRACompanyName
    {
        get
        {
            return _importData.GetSanitizedValue("FRA_Company_Name");
        }
    }

    public EFraRiskRating? FRAOutcome
    {
        get
        {
            return _importData.GetSanitizedValue("FRA_Outcome", true).ToFraRiskRating();
        }
    }

    public string FRARecommendations
    {
        get
        {
            return _importData.GetSanitizedValue("FRA_Recommendations");
        }
    }

    public bool HasFRAEW
    {
        get
        {
            var answer = _importData.GetSanitizedValue("Has_FRAEW", true);
            if (string.IsNullOrEmpty(answer))
            {
                return false;
            }
            return answer.StartsWith("y", StringComparison.CurrentCultureIgnoreCase);
        }
    }

    public string FRAEWCompanyName // Was CompanyWhoDidTheSurvey
    {
        get
        {
            return _importData.GetSanitizedValue("FRAEW_Company_Name");
        }
    }

    public ERiskType? FRAEWRiskLevel
    {
        get
        {
            var answer = _importData.GetSanitizedValue("FRAEW_Risk_Level", true)?.ToLower();

            if (answer == null)
            {
                return null;
            }

            if (answer.StartsWith("low"))
                return ERiskType.Low;
            if (answer.StartsWith("high"))
                return ERiskType.High;
            if (answer.Contains("tolerable"))
                return ERiskType.MediumTolerable;
            if (answer.Contains("action required"))
                return ERiskType.MediumActionRequired;
            return null;
        }
    }

    public EEvacuationStrategy? EvacuationStrategy
    {
        get
        {
            var answer = _importData.GetSanitizedValue("Evacuation_Strategy", true).ToEvacuationStrategyType();
            return answer;
        }
    }

    public EYesNoNonBoolean? HasInterimMeasures
    {
        get
        {
            return _importData.GetSanitizedValue("Has_Interim_Measures", true).ToYesNoNonBoolean();
        }
    }

    public IEnumerable<EInterimMeasuresType> BuildingInterimMeasuresTypes
    {
        get
        {
            var mappedTypes = new List<EInterimMeasuresType?>()
            {
                _importData.GetSanitizedValue("Interim_Measure_Type_1", true).ToInterimMeasuresType(),
                _importData.GetSanitizedValue("Interim_Measure_Type_2", true).ToInterimMeasuresType(),
                _importData.GetSanitizedValue("Interim_Measure_Type_3", true).ToInterimMeasuresType(),
                _importData.GetSanitizedValue("Interim_Measure_Type_4", true).ToInterimMeasuresType(),
                _importData.GetSanitizedValue("Interim_Measure_Type_5", true).ToInterimMeasuresType(),
                _importData.GetSanitizedValue("Interim_Measure_Type_6", true).ToInterimMeasuresType(),
                _importData.GetSanitizedValue("Interim_Measure_Type_7", true).ToInterimMeasuresType()
            };
            return mappedTypes.Where(mt => mt.HasValue).Select(mt => mt.Value).Distinct();
        }
    }
   
}

public static class HelperExtensions
{
    public static string GetSanitizedValue(this Dictionary<string, string> dictionary, string key, bool toLower = false)
    {
        // Normalize the input key: remove underscores, spaces, and make lowercase
        static string Normalize(string s) => new string([.. s.Where(c => !char.IsWhiteSpace(c) && c != '_')]).ToLowerInvariant();

        var normalizedKey = Normalize(key);

        foreach (var kvp in dictionary)
        {
            if (Normalize(kvp.Key) == normalizedKey)
            {
                if (kvp.Value == null)
                {
                    return null;
                }
                if (kvp.Value.StartsWith("null", StringComparison.CurrentCultureIgnoreCase) ||
                    kvp.Value.Equals("0", StringComparison.CurrentCultureIgnoreCase))
                {
                    return null;
                }
                return toLower ? kvp.Value.ToLower().Trim() : kvp.Value.Trim();
            }
        }

        return string.Empty;
    }

    public static EInterimMeasuresType? ToInterimMeasuresType(this string text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        if (text.Contains("alarm"))
            return EInterimMeasuresType.CommonFireAlarm;

        if (text.Contains("simultaneous"))
            return EInterimMeasuresType.SimultaneousEvacuationStrategy;

        if (text.Contains("evacuation"))
            return EInterimMeasuresType.EvacuationManagement;

        if (text.Contains("detection"))
            return EInterimMeasuresType.FireHeatSmokeDetection;

        if (text.Contains("supression"))
            return EInterimMeasuresType.FireSupressionSystem;

        if (text.Contains("applicable"))
            return EInterimMeasuresType.NotApplicable;

        if (text.Contains("waking"))
            return EInterimMeasuresType.WakingWatch;

        if (text.Contains("other"))
            return EInterimMeasuresType.Other;

        return null;
    }

    public static EEvacuationStrategy? ToEvacuationStrategyType(this string text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        if (text.Contains("applicable"))
            return EEvacuationStrategy.NotApplicable;

        if (text.Contains("captured"))
            return EEvacuationStrategy.NotCaptured;

        if (text.Contains("other"))
            return EEvacuationStrategy.Other;

        if (text.Contains("simultaneous"))
            return EEvacuationStrategy.SimultaneousEvacuation;

        if (text.Contains("stay"))
            return EEvacuationStrategy.StayPut;

        if (text.Contains("temporary"))
            return EEvacuationStrategy.TemporarySimultaneousEvacuation;

        return null;
    }

    public static EYesNoNonBoolean? ToYesNoNonBoolean(this string text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        if (text.Contains("know"))
            return EYesNoNonBoolean.DontKnow;

        if (text.Contains("yes"))
            return EYesNoNonBoolean.Yes;

        if (text.Contains("no"))
            return EYesNoNonBoolean.No;

        if (text.Contains("applicable"))
            return EYesNoNonBoolean.NotApplicable;

        return null;
    }

    public static EFraRiskRating? ToFraRiskRating(this string text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        if (text.Contains("high"))
            return EFraRiskRating.High;

        if (text.Contains("action"))
            return EFraRiskRating.MediumActionRequired;

        if (text.Contains("tolerable"))
            return EFraRiskRating.MediumTolerable;

        if (text.Contains("medium"))
            return EFraRiskRating.MediumActionRequired; // medium (no tolerable/action) = action

        if (text.Contains("low"))
            return EFraRiskRating.Low;

        return null;
    }

    public static EResponsibleEntityRelation? ToResponsibleEntityRelation(this string text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        if (text.Contains("leaseholder"))
            return EResponsibleEntityRelation.HeadLeaseholder;

        if (text.Contains("freeholder"))
            return EResponsibleEntityRelation.Freeholder;

        if (text.Contains("right") && text.Contains("manage"))
            return EResponsibleEntityRelation.RightToManageCompany;

        if (text.Contains("resident") && text.Contains("manage"))
            return EResponsibleEntityRelation.ResidentLedOrganisation;

        return null;
    }

    
}

using HE.Remediation.Core.Enums;
using System.Text.RegularExpressions;

namespace HE.Remediation.Core.UseCase.DataIngest
{
    public class ImportedDataRow(Dictionary<string, string> importData)
    {
        private readonly Dictionary<string, string> _importData = importData;

        // Building Details

        public string BuildingName { 
            get { 
                return _importData.GetSanitizedValue("Building_Name"); 
            } 
        }
        public string AddressLine1 { 
            get { 
                return _importData.GetSanitizedValue("Address_Line_1"); 
            } 
        }

        public string PostCode { 
            get { 
                return _importData.GetSanitizedValue("Postcode"); 
            } 
        }
        public string LocalAuthority { 
            get { 
                return _importData.GetSanitizedValue("Local_Authority"); 
            } 
        }

        public int? ResidentialUnitsCount
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

        public int? NumberOfStoreys
        {
            get {
                var heightBracket = _importData.GetSanitizedValue("Height_Bracket");
                if (string.IsNullOrEmpty(heightBracket))
                {
                    return null;
                }
                if (heightBracket.StartsWith("11-18m", StringComparison.OrdinalIgnoreCase))
                {
                    return 5;
                }
                if (heightBracket.StartsWith("18m+", StringComparison.OrdinalIgnoreCase))
                {
                    return 7;
                }

                return null;
            }
        }

        public string OriginalBuilderName { 
            get { 
                return _importData.GetSanitizedValue("Developer"); 
            } 
        }

        public bool KnowOriginalBuilder { 
            get { 
                return !string.IsNullOrEmpty(_importData.GetSanitizedValue("Developer")); 
            } 
        }

        // Responsible Entity

        public string OrganisationName { 
            get { 
                return _importData.GetSanitizedValue("RP_Name"); 
            } 
        }

        public EApplicationResponsibleEntityOrganisationType? OrganisationType
        {
            get
            {
                var answer = _importData.GetSanitizedValue("RE organisation type")?.ToLower();

                if (answer == null)
                {
                    return null;
                }

                if (answer.StartsWith("rp"))
                    return EApplicationResponsibleEntityOrganisationType.RegisteredProvider;
                if (answer.StartsWith("la"))
                    return EApplicationResponsibleEntityOrganisationType.LocalAuthority;
                return null;
            }
        }

        public string RegistrationNumber { 
            get { 
                return _importData.GetSanitizedValue("Provider_Number"); 
            } 
        }

        public int HowManyLeaseholders
        {
            get
            {
                var value = _importData.GetSanitizedValue("Leasehold");
                if (!int.TryParse(Regex.Replace(value ?? "", @"[^\d]", ""), out int result))
                {
                    return 0;
                }

                return result;
            }
        }

        public bool IsLeaseHoldersOrSharedOwners { 
            get { 
                return HowManyLeaseholders > 0; 
            } 
        }

        // FRAEW

        public bool HasCompletedFireRiskAppraisal
        {
            get
            {
                var answer = _importData.GetSanitizedValue("Specialist_Assessment_Undertaken");
                if (answer == null)
                {
                    return false;
                }
                return answer.StartsWith("Yes - a FRAEW", StringComparison.OrdinalIgnoreCase);
            }
        }

        public string CompanyWhoDidTheSurvey { 
            get { 
                return _importData.GetSanitizedValue("FRAEW_Company_Name"); 
            } 
        }

        public bool IsExternalWorks
        {
            get
            {
                var answer = _importData.GetSanitizedValue("Defects_Related_To")?.ToLower();
                if (answer == null)
                {
                    return false;
                }
                return answer.StartsWith("external wall system") || answer.StartsWith("ews") || answer.StartsWith("both");
            }
        }

        public bool IsInternalMitigationWorks
        {
            get
            {
                var answer = _importData.GetSanitizedValue("Defects_Related_To")?.ToLower();
                if (answer == null)
                {
                    return false;
                }
                return answer.StartsWith("other fire safety defects") || answer.StartsWith("ofsd") || answer.StartsWith("both");
            }
        }

        public string InternalElement { get { return _importData.GetSanitizedValue("Other_LCFS_Defect_Type"); } }

        public ERiskType? RiskLevel { 
            get {
                var answer = _importData.GetSanitizedValue("FRAEW_Risk_Level")?.ToLower();

                if(answer == null)
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

        public EReplacementCladding? CladdingReplacement
        {
            get
            {
                var answer = _importData.GetSanitizedValue("FRAEW_Recommendation")?.ToLower();

                if (answer == null)
                {
                    return null;
                }

                if (answer.StartsWith("full"))
                    return EReplacementCladding.Full;
                if (answer.StartsWith("partial"))
                    return EReplacementCladding.Partial;
                return null;
            }
        }
    }

    public static class HelperExtensions
    {
        public static string GetSanitizedValue(this Dictionary<string, string> dictionary, string key)
        {
            var result = dictionary.TryGetValue(key, out var value) ? value : string.Empty;

            if (result.StartsWith("null", StringComparison.CurrentCultureIgnoreCase) ||
                result.Equals("0", StringComparison.CurrentCultureIgnoreCase))
            {
                return null;
            }

            return result.Trim();
        }
    }
}

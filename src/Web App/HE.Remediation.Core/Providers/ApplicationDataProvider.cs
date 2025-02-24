using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.UserService.Enum;
using HE.Remediation.Core.Services.UserService.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.Providers
{
    public class ApplicationDataProvider : IApplicationDataProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtector _protector;
        private const string CookieName = "AppData";        
        
        private enum CookieValueTypes : int
        {
            UserIdCookieIndex = 0,
            AppIdCookieIndex = 1,
            Auth0UserCookieIndex = 2,
            EntityTypeCookieIndex = 3,
            ProfileCompletenessCookieIndex = 4,
            SessionTimeCookieIndex = 5,      
            DeclarationComplete = 6,
            ProgressReportIdCookieIndex = 7,
            PaymentRequestIdCookieIndex = 8,
            AppIdEmailAddressCookieIndex = 9
        }

        public ApplicationDataProvider(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider)
        {            
            _httpContextAccessor = httpContextAccessor;
            _protector = dataProtectionProvider.CreateProtector(nameof(ApplicationDataProvider));
        }

        public string GetCookieName => CookieName;

        /// <summary>
        /// Checks if we are on the enforced path - hence where the system controls our flow from start to
        /// finish. 
        /// </summary>
        /// <returns></returns>
        public bool IsEnforcedFlow()
        {
            UserProfileCompletionModel profileCompletion = GetProfileCompletion();

            // if we don't have a profile completion, we haven't started yet and must be on an enforced route for 
            // any user profile page accesses
            if (profileCompletion == null)
            {
                return true;
            }

            // We haven't set the profile type yet so we are on an enforced path
            if ((profileCompletion.ResponsibleEntityType != EResponsibleEntityType.Individual) &&
                (profileCompletion.ResponsibleEntityType != EResponsibleEntityType.Company))
            {
                return true;
            }

            if (profileCompletion.ResponsibleEntityType == EResponsibleEntityType.Company)
            {
                return (profileCompletion.IsSecondaryContactInformationComplete == false);            
            }

            return (profileCompletion.IsWantSecondaryContactComplete == false);
        }

        public Guid GetPaymentRequestId()
        {            
            var decryptedCookie = GetDecryptedCookie();
            if (decryptedCookie is null) return default;

            var cookieArray = decryptedCookie.Split("_");
            if ((int)(CookieValueTypes.PaymentRequestIdCookieIndex) >= cookieArray.Length)
            {
                return default;
            }

            var paymentRequestId = cookieArray[(int)(CookieValueTypes.PaymentRequestIdCookieIndex)];
            return Guid.TryParse(paymentRequestId, out var id)
                ? id
                : default;
        }

        public Guid GetProgressReportId()
        {            
            var decryptedCookie = GetDecryptedCookie();
            if (decryptedCookie is null) return default;

            var cookieArray = decryptedCookie.Split("_");
            if ((int)(CookieValueTypes.ProgressReportIdCookieIndex) >= cookieArray.Length)
            {
                return default;
            }

            var progressReportId = cookieArray[(int)(CookieValueTypes.ProgressReportIdCookieIndex)];
            return Guid.TryParse(progressReportId, out var id)
                ? id
                : default;
        }

        public Guid GetApplicationId()
        {            
            var decryptedCookie = GetDecryptedCookie();
            if (decryptedCookie is null) return default;

            var applicationId = decryptedCookie.Split("_")[(int)(CookieValueTypes.AppIdCookieIndex)];

            return Guid.TryParse(applicationId, out var id)
                ? id
                : default;
        }

        public Guid? GetUserId()
        {
            var decryptedCookie = GetDecryptedCookie();
            if (decryptedCookie is null) return default;

            var userId = decryptedCookie.Split("_")[(int)(CookieValueTypes.UserIdCookieIndex)];

            return Guid.TryParse(userId, out var id)
                ? id
                : null;
        }

        public EResponsibleEntityType? GetResponsibleEntityType()
        {
            var decryptedCookie = GetDecryptedCookie();
            if (decryptedCookie is null) return default;

            var entityTypePart = GetCookiePart((int)(CookieValueTypes.EntityTypeCookieIndex));
            if (entityTypePart is null) return default;

            EResponsibleEntityType tempType;
            if (Enum.TryParse(entityTypePart, out tempType))
            {
                if (Enum.IsDefined(typeof(EResponsibleEntityType), tempType))
                {
                    return tempType;
                }
            }            
            
            return default;
        }

        public void SetResponsibleEntityType(EResponsibleEntityType entityType)
        {            
            SetCookiePart((int)(CookieValueTypes.EntityTypeCookieIndex), ((int)entityType).ToString());
        }

        public UserProfileCompletionModel GetProfileCompletion()
        {
            var decryptedCookie = GetDecryptedCookie();
            if (decryptedCookie is null) return default;

            string profileCompleteStrValue = GetCookiePart((int)(CookieValueTypes.ProfileCompletenessCookieIndex));
            if (profileCompleteStrValue is null) return default;

            string entityTypePart = GetCookiePart((int)(CookieValueTypes.EntityTypeCookieIndex));
            if (entityTypePart is null) return default;

            var profileCompleteIntValue = ObtainIntFromText(profileCompleteStrValue);
            UserProfileCompletionModel profileCompletion = ObtainProfileCompletionValue(profileCompleteIntValue);                        

            EResponsibleEntityType entityType;
            if (Enum.TryParse(entityTypePart, out entityType))
            {
                if (Enum.IsDefined(typeof(EResponsibleEntityType), entityType))
                {
                    profileCompletion.ResponsibleEntityType = entityType;
                }
            }
            
            return profileCompletion;
        }

        public void SetProfileCompletion(UserProfileCompletionModel profileCompletion)
        {
            string profileCompletionValue = ProduceProfileCompletionValue(profileCompletion).ToString();
            SetCookiePart((int)(CookieValueTypes.ProfileCompletenessCookieIndex), profileCompletionValue);
        }

        public void SetUserProfileStageCompletionStatus(EUserProfileStage profileStage)
        {
            UserProfileCompletionModel profileCompletion = GetProfileCompletion();
            if (profileCompletion is null) return;

            switch (profileStage)
            {
                case EUserProfileStage.ContactInformation:

                    profileCompletion.IsContactInformationComplete = true;                                
                    break;
                case EUserProfileStage.CorrespondenceAddress:

                    profileCompletion.IsCorrespondenceAddressComplete = true;                                
                    break;
                case EUserProfileStage.ResponsibleEntityTypeSelection:

                    profileCompletion.IsResponsibleEntityTypeSelectionComplete = true;
                    break;
                case EUserProfileStage.CompanyDetails:

                    profileCompletion.IsCompanyDetailsComplete = true;
                    break;
                case EUserProfileStage.CompanyAddress:

                    profileCompletion.IsCompanyAddressComplete = true;
                    break;
                case EUserProfileStage.SecondaryContactInformation:

                    profileCompletion.IsSecondaryContactInformationComplete = true;
                    break;
                case EUserProfileStage.SecondaryContactSelection:

                    profileCompletion.IsSecondaryContactSelectionComplete = true;
                    break;
                case EUserProfileStage.ContactInfoConsent:

                    profileCompletion.IsContactConsentComplete = true;
                    break;
                case EUserProfileStage.WantSecondaryContact:

                    profileCompletion.IsWantSecondaryContactComplete = true;
                    break;
                case EUserProfileStage.UserAddedSecondaryContact:

                    profileCompletion.WantedToAddSecondaryContact = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(profileStage));
            }
            string profileCompletionValue = ProduceProfileCompletionValue(profileCompletion).ToString();
            SetCookiePart((int)(CookieValueTypes.ProfileCompletenessCookieIndex), profileCompletionValue);
        }

        public void SetUserProfileStageCompletionStatus(EUserProfileStage profileStage, EResponsibleEntityType entityType)
        {
            UserProfileCompletionModel profileCompletion = GetProfileCompletion();
            if (profileCompletion is null) return;

            switch (profileStage)
            {
                case EUserProfileStage.ContactInformation:

                    profileCompletion.IsContactInformationComplete = true;                                
                    break;
                case EUserProfileStage.CorrespondenceAddress:

                    profileCompletion.IsCorrespondenceAddressComplete = true;                                
                    break;
                case EUserProfileStage.ResponsibleEntityTypeSelection:

                    profileCompletion.IsResponsibleEntityTypeSelectionComplete = true;
                    break;
                case EUserProfileStage.CompanyDetails:

                    profileCompletion.IsCompanyDetailsComplete = true;
                    break;
                case EUserProfileStage.CompanyAddress:

                    profileCompletion.IsCompanyAddressComplete = true;
                    break;
                case EUserProfileStage.SecondaryContactInformation:

                    profileCompletion.IsSecondaryContactInformationComplete = true;
                    break;
                case EUserProfileStage.SecondaryContactSelection:

                    profileCompletion.IsSecondaryContactSelectionComplete = true;
                    break;
                case EUserProfileStage.ContactInfoConsent:

                    profileCompletion.IsContactConsentComplete = true;
                    break;
                case EUserProfileStage.WantSecondaryContact:

                    profileCompletion.IsWantSecondaryContactComplete = true;
                    break;
                case EUserProfileStage.UserAddedSecondaryContact:

                    profileCompletion.WantedToAddSecondaryContact = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(profileStage));
            }            
            var parts = GetCookieParts();
            parts[(int)(CookieValueTypes.ProfileCompletenessCookieIndex)] = ProduceProfileCompletionValue(profileCompletion).ToString();
            parts[(int)(CookieValueTypes.EntityTypeCookieIndex)] = ((int)entityType).ToString();
            parts[(int)(CookieValueTypes.SessionTimeCookieIndex)] = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            SetCookieValue(string.Join("_", parts));
        }
        
        public void SetAuth0UserId(string auth0UserId)
        {
            SetCookiePart((int)(CookieValueTypes.Auth0UserCookieIndex), auth0UserId);
        }

        public string GetAuth0UserId()
        {
            var decryptedCookie = GetDecryptedCookie();
            if (decryptedCookie is null) return string.Empty;

            var auth0UserId = decryptedCookie.Split("_")[(int)(CookieValueTypes.Auth0UserCookieIndex)];
            return auth0UserId;
        }

        public DateTimeOffset? GetSessionTimeout()
        {
            var decryptedCookie = GetDecryptedCookie();
            if (decryptedCookie is null) return null;

            var sessionTime = long.Parse(decryptedCookie.Split("_")[(int)(CookieValueTypes.SessionTimeCookieIndex)]);
            return DateTimeOffset.FromUnixTimeSeconds(sessionTime);
        }

        public void SetUserDetails(Guid userId, string auth0UserId, 
                                   UserProfileCompletionModel profileCompletion)
        {
            var parts = GetCookieParts();
            parts[(int)(CookieValueTypes.UserIdCookieIndex)] = userId.ToString();
            parts[(int)(CookieValueTypes.Auth0UserCookieIndex)] = auth0UserId;
            parts[(int)(CookieValueTypes.EntityTypeCookieIndex)] = ((int)profileCompletion.ResponsibleEntityType).ToString();
            parts[(int)(CookieValueTypes.ProfileCompletenessCookieIndex)] = ProduceProfileCompletionValue(profileCompletion).ToString();
            parts[(int)(CookieValueTypes.SessionTimeCookieIndex)] = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            SetCookieValue(string.Join("_", parts));
        }

        public void SetUserId(Guid userId)
        {            
            var currentParts = GetCookieParts();
            currentParts[(int)(CookieValueTypes.UserIdCookieIndex)] = userId.ToString();
            currentParts[(int)(CookieValueTypes.SessionTimeCookieIndex)] = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            SetCookieValue(string.Join("_", currentParts));
        }

        public void SetPaymentRequestId(Guid paymentRequestId)
        {
            var currentParts = GetCookieParts();
            currentParts[(int)(CookieValueTypes.PaymentRequestIdCookieIndex)] = paymentRequestId.ToString();
            SetCookieValue(string.Join("_", currentParts));
        }

        public void SetProgressReportId(Guid progressReportId)
        {
            var currentParts = GetCookieParts();
            currentParts[(int)(CookieValueTypes.ProgressReportIdCookieIndex)] = progressReportId.ToString();
            SetCookieValue(string.Join("_", currentParts));
        }

        public void SetApplicationId(Guid applicationId)
        {
            SetCookiePart((int)(CookieValueTypes.AppIdCookieIndex), applicationId.ToString());
        }

        public void SetApplicationIdAndEmailAddress(Guid applicationId, string emailAddress)
        {
            Dictionary<int, string> indexesAndValues = new Dictionary<int, string>()
            {
                {(int)(CookieValueTypes.AppIdCookieIndex), applicationId.ToString()},
                {(int)(CookieValueTypes.AppIdEmailAddressCookieIndex), emailAddress}
            };

            SetCookieParts(indexesAndValues);
        }

        public string GetApplicationEmailAddress()
        {
            var decryptedCookie = GetDecryptedCookie();
            if (decryptedCookie is null) return default;

            var emailAddress = decryptedCookie.Split("_")[(int)(CookieValueTypes.AppIdEmailAddressCookieIndex)];

            return emailAddress;
        }

        public void SetSessionTimeout()
        {
            var currentParts = GetCookieParts();
            currentParts[(int)(CookieValueTypes.SessionTimeCookieIndex)] = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            SetCookieValue(string.Join("_", currentParts));
        }

        private string GetDecryptedCookie()
        {            
            var encryptedCookie = _httpContextAccessor.HttpContext?.Request.Cookies[CookieName];
            if (encryptedCookie == null) return null;

            var decryptedCookie = _protector.Unprotect(encryptedCookie);
            return decryptedCookie;
        }

        private string GetCookiePart(int index)
        {
            var parts = GetCookieParts();
            if (parts.Length > index)
            {
                return parts[index];            
            }
            return string.Empty;
        }

        private string[] GetCookieParts()
        {
            var noOfCookieIndexes = Enum.GetValues(typeof(CookieValueTypes)).Length;
            var decryptedCookie = GetDecryptedCookie()?.Split("_");
            if (decryptedCookie == null) return new string[ noOfCookieIndexes ];

            if (decryptedCookie.Length < noOfCookieIndexes) 
            { 
                Array.Resize<string> (ref decryptedCookie, 
                                      noOfCookieIndexes);
            }
            return decryptedCookie;
        }

        private void SetCookieValue(string value)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1),
                Path = "/",
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                HttpOnly = true,
            };

            var encryptedAppId = _protector.Protect(value);
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(CookieName, encryptedAppId, cookieOptions);                        
        }

        private void SetCookiePart(int index, string value)
        {            
            var parts = GetCookieParts();
            if (index < parts.Length)
            {
                parts[index] = value;
                SetCookieValue(string.Join("_", parts));
            }            
        }

        private void SetCookieParts(IDictionary<int, string> indexAndValues)
        {            
            var parts = GetCookieParts();
            foreach (var indexAndValue in indexAndValues)
            {
                if (indexAndValue.Key < parts.Length)
                {
                    parts[indexAndValue.Key] = indexAndValue.Value;
                    SetCookieValue(string.Join("_", parts));
                }
            }
        }
        
        private UserProfileCompletionModel ObtainProfileCompletionValue(int profileCompletionValue)
        {
            UserProfileCompletionModel profileCompletion = new UserProfileCompletionModel();
            profileCompletion.IsContactInformationComplete = (profileCompletionValue & 1) == 1;
            profileCompletion.IsCorrespondenceAddressComplete = (profileCompletionValue & 2) == 2;
            profileCompletion.IsResponsibleEntityTypeSelectionComplete = (profileCompletionValue & 4) == 4;
            profileCompletion.IsCompanyDetailsComplete = (profileCompletionValue & 8) == 8;
            profileCompletion.IsCompanyAddressComplete = (profileCompletionValue & 16) == 16;
            profileCompletion.IsSecondaryContactInformationComplete = (profileCompletionValue & 32) == 32;
            profileCompletion.IsSecondaryContactSelectionComplete = (profileCompletionValue & 64) == 64;
            profileCompletion.IsContactConsentComplete = (profileCompletionValue & 128) == 128;
            profileCompletion.IsWantSecondaryContactComplete = (profileCompletionValue & 256) == 256;
            profileCompletion.WantedToAddSecondaryContact = (profileCompletionValue & 512) == 512;
            return profileCompletion;
        }

        private int ProduceProfileCompletionValue(UserProfileCompletionModel profileCompletion)
        {
            int profileCompletionValue = 0;            
            profileCompletionValue |= (profileCompletion.IsContactInformationComplete ? 1 : 0);
            profileCompletionValue |= (profileCompletion.IsCorrespondenceAddressComplete == true ? 2 : 0);
            profileCompletionValue |= (profileCompletion.IsResponsibleEntityTypeSelectionComplete ? 4 : 0);            
            profileCompletionValue |= (profileCompletion.IsCompanyDetailsComplete == true) ? 8 : 0;
            profileCompletionValue |= (profileCompletion.IsCompanyAddressComplete == true) ? 16 : 0;
            profileCompletionValue |= (profileCompletion.IsSecondaryContactInformationComplete == true ? 32 : 0);            
            profileCompletionValue |= (profileCompletion.IsSecondaryContactSelectionComplete == true ? 64 : 0);     
            profileCompletionValue |= (profileCompletion.IsContactConsentComplete == true ? 128 : 0);            
            profileCompletionValue |= (profileCompletion.IsWantSecondaryContactComplete == true ? 256 : 0);            
            profileCompletionValue |= (profileCompletion.WantedToAddSecondaryContact == true ? 512 : 0);            
            return profileCompletionValue;
        }

        private int ObtainIntFromText(string inputText)
        {
            try
            {
                return Int32.Parse(inputText);
            }
            catch (FormatException)
            {
                return -1;
            }            
        }
    }
}

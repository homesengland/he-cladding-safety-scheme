namespace HE.Remediation.WebApp.CustomPropertyValidators
{
    public static class ValidUkBasedPostcode
    {
        public static bool BeAUkBasedPostcode(string postCode)
        {
            List<string> excludedPostcodes = new List<string>();
            excludedPostcodes.Add("JE");
            excludedPostcodes.Add("GY");
            excludedPostcodes.Add("IM");

            return !excludedPostcodes.Contains(postCode.Substring(0, 2));
        }
    }
}

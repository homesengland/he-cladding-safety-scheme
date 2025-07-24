namespace HE.Remediation.Core.UseCase.DataIngest.Validation
{
    public static class ValidUkBasedPostcode
    {
        private static readonly string[] ExcludedPostcodes = { "JE", "GY", "IM" };

        public static bool BeAUkBasedPostcode(string postCode)
        {
            return !string.IsNullOrWhiteSpace(postCode) && !ExcludedPostcodes.Contains(postCode.Substring(0, 2));
        }
    }
}

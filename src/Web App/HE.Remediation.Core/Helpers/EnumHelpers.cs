using System.Text.RegularExpressions;

namespace HE.Remediation.Core.Helpers
{
    public static class EnumHelpers
    {
        /// <summary>
        /// Converts an enum camel-case string value to a spaced, sentence-cased string for display purposes, with handler for punctuation exceptions.
        /// </summary>
        /// <typeparam name="T">System.Enum</typeparam>
        /// <param name="enumValue">Enum value</param>
        /// <returns>A spaced, sentence-cased String</returns>
        public static string GetText<T>(T enumValue, bool useSentenceCase = true) where T : Enum
        {
            var formattedEnumStrings = Enum
                        .GetValues(typeof(T))
                        .Cast<T>()
                        .ToDictionary(
                            t => t,
                            t => Regex.Replace(t.ToString(), "[a-z][A-Z]", m => $"{m.Value[0]} {(useSentenceCase ? char.ToLower(m.Value[1]) : m.Value[1])}"));

            formattedEnumStrings.TryGetValue(enumValue, out string formattedString);

            //add case to handle punctuation exceptions where required
            formattedString = formattedString switch
            {
                string a when a.Contains("Dont") => formattedString.Replace("Dont", "Don't"),
                //add case
                _ => formattedString
            };

            return formattedString;
        }
    }
}
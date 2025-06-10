using System;
using System.Text.RegularExpressions;

namespace HE.Remediation.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SplitCamelCase(this string input)
        {
            return Regex.Replace(input, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled);
        }

        public static string ToSentenceCase(this string input)
        {
            return input is not null
                ? string.Concat(input[0].ToString().ToUpper(), input[1..].ToLower())
                : null;
        }
        public static string SplitCamelCaseAllButFirstWordLowercase(this string input)
        {
            return input.SplitCamelCase().ToSentenceCase();
        }
    }
}
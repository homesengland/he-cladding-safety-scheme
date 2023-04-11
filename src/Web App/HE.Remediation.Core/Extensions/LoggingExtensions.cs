using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.Extensions
{
    internal static partial class LoggingExtensions
    {
        [LoggerMessage(1, LogLevel.Debug, "Reading data with key '{FriendlyName}', value '{Value}'.",
            EventName = "ReadKeyFromElement")]
        public static partial void ReadingXmlFromKey(this ILogger logger, string friendlyName, string value);

        [LoggerMessage(2, LogLevel.Debug, "Saving key '{FriendlyName}' to database'.",
            EventName = "SavingKeyToDatabase")]
        public static partial void LogSavingKeyToDatabase(this ILogger logger, string friendlyName);
    }
}

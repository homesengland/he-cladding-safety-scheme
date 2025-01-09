namespace HE.Remediation.Core.Enums;

public enum EGovNotifyNotificationStatusType
{
    // Email
    Email_Created = 1,
    Email_Sending = 2,
    Email_Delivered = 3,
    Email_PermanentFailure = 4,
    Email_TemporaryFailure = 5,
    Email_TechnicalFailure = 6,

    // Letter
    Letter_Accepted = 7,
    Letter_Received = 8,
    Letter_Cancelled = 9,
    Letter_PendingVirusCheck = 10,
    Letter_VirusScanFailed = 11,
    Letter_ValidationFailed = 12,
    Letter_TechnicalFailure = 13,
    Letter_PermanentFailure = 14,
}
public class AddressResolverException : DataImportException
{
    public ErrorType LookupErrorType { get; }

    public AddressResolverException(string message, ErrorType errorType) : base($"Postcode Lookup: {message}")
    {
        LookupErrorType = errorType;
    }

    public enum ErrorType
    {
        Postcode,
        BuildingName,
        Other
    }
}
namespace HE.Remediation.Core.Exceptions
{
    public class InvalidFileException: Exception
    {
        public InvalidFileException(string message): base(message)
        {

        }

        public InvalidFileException(string message, string propertyName)
        {
            Errors = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(propertyName, message)
            };
        }

        public InvalidFileException(List<KeyValuePair<string, string>> errors)
        {
            Errors = errors;
        }

        public List<KeyValuePair<string, string>> Errors { get; set; }
    }
}

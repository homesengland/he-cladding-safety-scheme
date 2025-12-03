namespace HE.Remediation.Core.UseCase.DataIngest.RAS;

public class DataImportException : Exception
{
    public DataImportException(string message) : base(message)
    {
    }

    public string ColumnName { get; set; }
}

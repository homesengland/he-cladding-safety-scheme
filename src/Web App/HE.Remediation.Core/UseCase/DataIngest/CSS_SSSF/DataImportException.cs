namespace HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF;

public class DataImportException : Exception
{
    public DataImportException(string message) : base(message)
    {
    }

    public string ColumnName { get; set; }
}

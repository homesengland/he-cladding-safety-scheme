public class DataImportException : Exception
{
    public DataImportException(string message) : base(message)
    {
    }

    public string ColumnName { get; set; }
}

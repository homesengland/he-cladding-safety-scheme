using System.Data;
using Serilog;
using Serilog.Sinks.MSSqlServer;


namespace HE.Remediation.Core.Middleware;

public static class LoggerConfigurationExtensions
{
    public static LoggerConfiguration WriteToSql(this LoggerConfiguration loggerConfiguration, string connectionString)
    {
        loggerConfiguration.WriteTo.MSSqlServer(
            connectionString: connectionString,
            sinkOptions: new MSSqlServerSinkOptions
            {
                AutoCreateSqlTable = false, // created by Database solution
                TableName = "SystemLog"
            },
            columnOptions: ConfigureSerilogColumnOptions(),
            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning
        );

        return loggerConfiguration;
    }


    private static ColumnOptions ConfigureSerilogColumnOptions()
    {
        var properties = new ColumnOptions();

        properties.Store.Remove(StandardColumn.Properties);
        properties.Store.Add(StandardColumn.LogEvent);

        properties.DisableTriggers = true;

        properties.Level.ColumnName = "Severity";
        properties.Level.StoreAsEnum = false;

        properties.TimeStamp.ColumnName = "TimeStampUtc";
        properties.TimeStamp.ConvertToUtc = true;

        properties.LogEvent.ExcludeAdditionalProperties = false;

        properties.AdditionalColumns = new List<SqlColumn>
        {
            new() { ColumnName = "Elapsed", DataType = SqlDbType.Decimal, AllowNull = true },
            new() { ColumnName = "RequestMethod", DataType = SqlDbType.NVarChar, AllowNull = true },
            new() { ColumnName = "RequestPath", DataType = SqlDbType.NVarChar, AllowNull = true },
            new() { ColumnName = "RequestId", DataType = SqlDbType.NVarChar, AllowNull = true },
            new() { ColumnName = "StatusCode", DataType = SqlDbType.Int, AllowNull = true },
            new() { ColumnName = "Source", DataType = SqlDbType.NVarChar, DataLength = 25, AllowNull = true },
            new() { ColumnName = "UserId", DataType = SqlDbType.NVarChar, DataLength = 50, AllowNull = true }
        };

        return properties;
    }
}
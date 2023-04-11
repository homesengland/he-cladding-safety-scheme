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
                AutoCreateSqlTable = true,
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
            new SqlColumn { ColumnName = "Elapsed", DataType = SqlDbType.Decimal, AllowNull = true },
            new SqlColumn { ColumnName = "RequestMethod", DataType = SqlDbType.NVarChar, AllowNull = true },
            new SqlColumn { ColumnName = "RequestPath", DataType = SqlDbType.NVarChar, AllowNull = true },
            new SqlColumn { ColumnName = "RequestId", DataType = SqlDbType.NVarChar, AllowNull = true },
            new SqlColumn { ColumnName = "StatusCode", DataType = SqlDbType.Int, AllowNull = true },
        };

        return properties;
    }
}
using Dapper;
using HE.Remediation.Core.Interface;
using System.Data;

namespace HE.Remediation.Core.Data
{
    public class DbConnectionWrapper : IDbConnectionWrapper
    {
        private readonly IDbConnection _dbConnection;

        public DbConnectionWrapper(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IReadOnlyCollection<T>> QueryAsync<T>(string sprocName, object parameters = null)
        {
            var results = await _dbConnection.QueryAsync<T>(sprocName, parameters, commandType: CommandType.StoredProcedure);
            return results.ToArray();
        }

        public async Task ExecuteAsync(string sprocName, object parameters = null)
        {
            await _dbConnection.ExecuteAsync(sprocName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sprocName, object parameters = null)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(sprocName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IReadOnlyCollection<T>> QueryAsync<T1, T2, T>(string sprocName, Func<T1, T2, T> map, object parameters = null, string splitOn = "Id")
        {
            var results = await _dbConnection.QueryAsync(sprocName, map, parameters, commandType: CommandType.StoredProcedure, splitOn: splitOn );
            return results.ToArray();
        }
    }
}

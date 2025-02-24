namespace HE.Remediation.Core.Interface
{
    public interface IDbConnectionWrapper
    {
        Task<IReadOnlyCollection<T>> QueryAsync<T>(string sprocName, object parameters = null);
        Task ExecuteAsync(string sprocName, object parameters = null);
        Task<T> QuerySingleOrDefaultAsync<T>(string sprocName, object parameters = null);
        Task<IReadOnlyCollection<T>> QueryAsync<T1, T2, T>(string sprocName, Func<T1, T2, T> map, object parameters = null, string splitOn = "Id");     
    }
}
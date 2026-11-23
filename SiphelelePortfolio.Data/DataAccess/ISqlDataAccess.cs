namespace SiphelelePortfolio.Data.DataAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> QueryAsync<T>(string sp, object? parameters = null);
    Task<T?> QuerySingleAsync<T>(string sp, object? parameters = null);
    Task<T?> ExecuteScalarAsync<T>(string sp, object? parameters = null);   // ← T?
    Task<int> ExecuteAsync(string sp, object? parameters = null);
}
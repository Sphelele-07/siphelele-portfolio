using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SiphelelePortfolio.Data.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly string _connectionString;

    public SqlDataAccess(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Missing 'DefaultConnection'.");
    }

    private IDbConnection Create() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<T>> QueryAsync<T>(string sp, object? parameters = null)
    {
        using var conn = Create();
        return await conn.QueryAsync<T>(sp, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<T?> QuerySingleAsync<T>(string sp, object? parameters = null)
    {
        using var conn = Create();
        return await conn.QuerySingleOrDefaultAsync<T>(sp, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<T?> ExecuteScalarAsync<T>(string sp, object? parameters = null)
    {
        using var conn = Create();
        return await conn.ExecuteScalarAsync<T>(sp, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> ExecuteAsync(string sp, object? parameters = null)
    {
        using var conn = Create();
        return await conn.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure);
    }
} 
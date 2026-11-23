using SiphelelePortfolio.Data.DataAccess;
using SiphelelePortfolio.Data.Models.Domain;
using SiphelelePortfolio.Data.Repositories.Interfaces;

namespace SiphelelePortfolio.Data.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly ISqlDataAccess _db;
    public UserRepository(ISqlDataAccess db) => _db = db;

    public async Task<User?> GetByUsernameAsync(string username)
        => await _db.QuerySingleAsync<User>("usp_User_GetByUsername", new { Username = username });

    public async Task<User?> GetByIdAsync(int userId)
        => await _db.QuerySingleAsync<User>("usp_User_GetById", new { UserId = userId });

    public async Task UpdateLastLoginAsync(int userId, DateTime when)
        => await _db.ExecuteAsync("usp_User_UpdateLastLogin", new { UserId = userId, LastLoginAt = when });
}
using SiphelelePortfolio.Data.Models.Domain;

namespace SiphelelePortfolio.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(int userId);
    Task UpdateLastLoginAsync(int userId, DateTime when);
}
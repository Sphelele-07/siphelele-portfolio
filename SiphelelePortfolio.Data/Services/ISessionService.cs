using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace SiphelelePortfolio.Data.Services;

public interface ISessionService
{
    bool IsAuthenticated { get; }
    int? UserId { get; }
    string? Username { get; }
    void SignIn(int userId, string username);
    void SignOut();
}
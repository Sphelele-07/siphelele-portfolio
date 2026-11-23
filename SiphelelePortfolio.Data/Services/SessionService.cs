using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace SiphelelePortfolio.Data.Services;

public class SessionService : ISessionService
{
    private const string KeyUserId = "auth.userid";
    private const string KeyUsername = "auth.username";

    private readonly IHttpContextAccessor _http;
    public SessionService(IHttpContextAccessor http) => _http = http;

    private ISession Session => _http.HttpContext?.Session
        ?? throw new InvalidOperationException("Session is not available.");

    public bool IsAuthenticated => Session.GetInt32(KeyUserId).HasValue;
    public int? UserId => Session.GetInt32(KeyUserId);
    public string? Username => Session.GetString(KeyUsername);

    public void SignIn(int userId, string username)
    {
        Session.SetInt32(KeyUserId, userId);
        Session.SetString(KeyUsername, username);
    }

    public void SignOut() => Session.Clear();
}
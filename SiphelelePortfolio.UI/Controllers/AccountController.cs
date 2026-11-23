using Microsoft.AspNetCore.Mvc;
using SiphelelePortfolio.Data.Models.ViewModels;
using SiphelelePortfolio.Data.Repositories.Interfaces;
using SiphelelePortfolio.Data.Services;

namespace SiphelelePortfolio.UI.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly ISessionService _session;

    public AccountController(IUserRepository users, IPasswordHasher hasher, ISessionService session)
    {
        _users = users;
        _hasher = hasher;
        _session = session;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (_session.IsAuthenticated) return RedirectToAction("Index", "Admin");
        return View(new LoginVM { ReturnUrl = returnUrl });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var user = await _users.GetByUsernameAsync(vm.Username.Trim().ToLowerInvariant());
        var ok = user is not null && _hasher.Verify(vm.Password, user.PasswordHash);

        if (!ok)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(vm);
        }

        _session.SignIn(user!.UserId, user.Username);
        await _users.UpdateLastLoginAsync(user.UserId, DateTime.UtcNow);

        if (!string.IsNullOrWhiteSpace(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
            return Redirect(vm.ReturnUrl);

        return RedirectToAction("Index", "Admin");
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        _session.SignOut();
        return RedirectToAction("Index", "Home");
    }
}
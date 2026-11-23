using Microsoft.AspNetCore.Mvc;
using SiphelelePortfolio.Data.Models.Domain;
using SiphelelePortfolio.Data.Models.ViewModels;
using SiphelelePortfolio.Data.Repositories.Interfaces;
using SiphelelePortfolio.Data.Services;

namespace SiphelelePortfolio.UI.Controllers;

public class CollaborationController : Controller
{
    private readonly ICollaborationRepository _collabs;
    private readonly IEmailService _email;

    public CollaborationController(ICollaborationRepository collabs, IEmailService email)
    {
        _collabs = collabs;
        _email = email;
    }

    [HttpGet]
    public IActionResult Index() => View(new CollaborationVM());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CollaborationVM vm)
    {
        if (!ModelState.IsValid) return View(vm);

        await _collabs.InsertAsync(new Collaboration
        {
            FullName = vm.FullName.Trim(),
            Email = vm.Email.Trim().ToLowerInvariant(),
            Subject = vm.Subject.Trim(),
            Message = vm.Message.Trim()
        });

        try { await _email.NotifyNewCollaborationAsync(vm.FullName, vm.Email, vm.Subject, vm.Message); }
        catch { }

        TempData["Success"] = "Thank you! Your message has been received. I'll get back to you within 24 hours.";
        return RedirectToAction(nameof(Index));
    }
}
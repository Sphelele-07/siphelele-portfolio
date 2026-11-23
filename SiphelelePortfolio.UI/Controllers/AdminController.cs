using Microsoft.AspNetCore.Mvc;
using SiphelelePortfolio.Data.Models.Domain;
using SiphelelePortfolio.Data.Models.ViewModels;
using SiphelelePortfolio.Data.Repositories.Interfaces;
using SiphelelePortfolio.UI.Filters;

namespace SiphelelePortfolio.UI.Controllers;

[AdminAuth]
public class AdminController : Controller
{
    private readonly IEngagementRepository _engagements;
    private readonly ICollaborationRepository _collabs;
    private readonly ICertificateRepository _certificates;
    private readonly IProjectRepository _projects;

    public AdminController(
        IEngagementRepository engagements,
        ICollaborationRepository collabs,
        ICertificateRepository certificates,
        IProjectRepository projects)
    {
        _engagements = engagements;
        _collabs = collabs;
        _certificates = certificates;
        _projects = projects;
    }

    // ── DASHBOARD ─────────────────────────────────────────────────
    public async Task<IActionResult> Index(string period = "6months")
    {
        int months = period == "3months" ? 3 : period == "12months" ? 12 : 6;

        var engagements = (await _engagements.GetStatsByMonthAsync(months)).ToList();
        var collabs = (await _collabs.GetMonthlyStatsAsync(months)).ToList();

        var labels = engagements.Select(m => m.YearMonth)
            .Union(collabs.Select(m => m.YearMonth))
            .OrderBy(m => m).ToList();

        var vm = new AdminDashboardVM
        {
            UnreadCollaborations = await _collabs.GetUnreadCountAsync(),
            TotalEngagements = (await _engagements.GetAllAsync()).Count(),
            TotalCollaborations = (await _collabs.GetAllAsync()).Count(),
            ChartLabels = labels,
            EngagementData = labels.Select(l => engagements.FirstOrDefault(x => x.YearMonth == l).TotalEngagements).ToList(),
            CollaborationData = labels.Select(l => collabs.FirstOrDefault(x => x.YearMonth == l).TotalCollabs).ToList()
        };

        ViewData["Title"] = "Dashboard";
        ViewData["CurrentPeriod"] = period;
        return View(vm);
    }

    // ── ENGAGEMENTS ───────────────────────────────────────────────
    public async Task<IActionResult> Engagements()
    {
        var data = (await _engagements.GetAllAsync()).ToList();
        ViewData["Title"] = "Engagements";
        return View(data);
    }

    // ── COLLABORATIONS ────────────────────────────────────────────
    public async Task<IActionResult> Collaborations()
    {
        var data = (await _collabs.GetAllAsync()).ToList();
        ViewData["Title"] = "Collaboration Inbox";
        return View(data);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkRead(int id)
    {
        await _collabs.MarkReadAsync(id);
        return RedirectToAction(nameof(Collaborations));
    }

    // ── CERTIFICATES CRUD ─────────────────────────────────────────
    public async Task<IActionResult> Certificates()
    {
        var data = (await _certificates.GetAllAsync()).ToList();
        ViewData["Title"] = "Certificates";
        return View(data);
    }

    [HttpGet]
    public IActionResult CertificateCreate()
    {
        ViewData["Title"] = "Add Certificate";
        return View(new Certificate { IssueDate = DateTime.Today });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CertificateCreate(Certificate model)
    {
        if (!ModelState.IsValid) return View(model);
        await _certificates.InsertAsync(model);
        TempData["Success"] = "Certificate added.";
        return RedirectToAction(nameof(Certificates));
    }

    [HttpGet]
    public async Task<IActionResult> CertificateEdit(int id)
    {
        var cert = await _certificates.GetByIdAsync(id);
        if (cert == null) return NotFound();
        ViewData["Title"] = "Edit Certificate";
        return View("CertificateCreate", cert);   // ← same view
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CertificateEdit(Certificate model)
    {
        if (!ModelState.IsValid) return View(model);
        await _certificates.UpdateAsync(model);
        TempData["Success"] = "Certificate updated.";
        return RedirectToAction(nameof(Certificates));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CertificateDelete(int id)
    {
        await _certificates.DeleteAsync(id);
        TempData["Success"] = "Certificate deleted.";
        return RedirectToAction(nameof(Certificates));
    }

    // ── PROJECTS CRUD ─────────────────────────────────────────────
    public async Task<IActionResult> Projects()
    {
        var data = (await _projects.GetAllAsync()).ToList();
        ViewData["Title"] = "Projects";
        return View(data);
    }

    [HttpGet]
    public IActionResult ProjectCreate()
    {
        ViewData["Title"] = "Add Project";
        return View(new Project { DisplayOrder = 1 });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ProjectCreate(Project model)
    {
        if (!ModelState.IsValid) return View(model);
        await _projects.InsertAsync(model);
        TempData["Success"] = "Project added.";
        return RedirectToAction(nameof(Projects));
    }

    [HttpGet]
    public async Task<IActionResult> ProjectEdit(int id)
    {
        var p = await _projects.GetByIdAsync(id);
        if (p == null) return NotFound();
        ViewData["Title"] = "Edit Project";
        return View("ProjectCreate", p);   // ← same view
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ProjectEdit(Project model)
    {
        if (!ModelState.IsValid) return View(model);
        await _projects.UpdateAsync(model);
        TempData["Success"] = "Project updated.";
        return RedirectToAction(nameof(Projects));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ProjectDelete(int id)
    {
        await _projects.DeleteAsync(id);
        TempData["Success"] = "Project deleted.";
        return RedirectToAction(nameof(Projects));
    }
}
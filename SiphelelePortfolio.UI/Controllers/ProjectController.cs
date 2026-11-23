using Microsoft.AspNetCore.Mvc;
using SiphelelePortfolio.Data.Models.Domain;
using SiphelelePortfolio.Data.Repositories.Interfaces;
using SiphelelePortfolio.Data.Services;

namespace SiphelelePortfolio.UI.Controllers;

[Route("api/[controller]")]
public class ProjectController : Controller
{
    private readonly IEngagementRepository _engagements;
    private readonly IProjectRepository _projects;
    private readonly IPdfService _pdf;
    private readonly IHttpContextAccessor _http;

    public ProjectController(
        IEngagementRepository engagements,
        IProjectRepository projects,
        IPdfService pdf,
        IHttpContextAccessor http)
    {
        _engagements = engagements;
        _projects = projects;
        _pdf = pdf;
        _http = http;
    }

    [HttpPost("log/{eventType}")]
    public async Task<IActionResult> Log(string eventType, int projectId = 0, string? projectTitle = null)
    {
        await _engagements.LogAsync(new Engagement
        {
            EventType = eventType,
            TargetId = projectId > 0 ? projectId : null,
            TargetLabel = projectTitle,
            UserAgent = _http.HttpContext?.Request.Headers["User-Agent"].ToString(),
            IpAddress = _http.HttpContext?.Connection.RemoteIpAddress?.ToString()
        });
        return Ok();
    }

    [HttpGet("download-requirements/{id}")]
    public async Task<IActionResult> DownloadRequirements(int id)
    {
        var project = await _projects.GetByIdAsync(id);
        if (project == null) return NotFound();

        await _engagements.LogAsync(new Engagement
        {
            EventType = "DownloadRequirements",
            TargetId = id,
            TargetLabel = project.Title
        });

        var pdfBytes = _pdf.GenerateRequirementsPdf(project);
        return File(pdfBytes, "application/pdf", $"{project.Title}-Requirements.pdf");
    }
}
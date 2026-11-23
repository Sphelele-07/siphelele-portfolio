using Microsoft.AspNetCore.Mvc;
using SiphelelePortfolio.Data.Models.Domain;
using SiphelelePortfolio.Data.Models.ViewModels;
using SiphelelePortfolio.Data.Repositories.Interfaces;

namespace SiphelelePortfolio.UI.Controllers;

public class HomeController : Controller
{
    private readonly IProjectRepository _projects;
    private readonly ICertificateRepository _certificates;
    private readonly IEngagementRepository _engagements;
    private readonly IHttpContextAccessor _http;

    public HomeController(
        IProjectRepository projects,
        ICertificateRepository certificates,
        IEngagementRepository engagements,
        IHttpContextAccessor http)
    {
        _projects = projects;
        _certificates = certificates;
        _engagements = engagements;
        _http = http;
    }

    public async Task<IActionResult> Index()
    {
        await _engagements.LogAsync(new Engagement
        {
            EventType = "PageView",
            TargetLabel = "Home",
            UserAgent = _http.HttpContext?.Request.Headers["User-Agent"].ToString(),
            IpAddress = _http.HttpContext?.Connection.RemoteIpAddress?.ToString()
        });

        var vm = new HomeVM
        {
            Projects = (await _projects.GetAllAsync()).ToList(),
            Certificates = (await _certificates.GetAllAsync()).ToList(),

            // ── TECHNICAL SKILLS ────────────────────────────────
            TechnicalSkills = new List<SkillGroup>
        {
            new() { Category = "Backend", Items = new() { "C# (ASP.NET MVC)", "Python", "Algorithms", "JSON", "3-Tier Architecture" } },
            new() { Category = "Frontend", Items = new() { "HTML", "CSS", "Bootstrap" } },
            new() { Category = "Database", Items = new() { "SQL Server", "MS Access", "Dapper" } },
            new() { Category = "Testing", Items = new() { "Manual Testing", "Test Case Design", "ISTQB Principles" } },
            new() { Category = "Data", Items = new() { "Machine Learning", "Linear Regression", "Data Analysis" } },
            new() { Category = "Tools", Items = new() { "GitHub", "Visual Studio", "Excel", "Word", "PowerPoint" } }
        },

            // ── SYSTEMS WORKED WITH ─────────────────────────────
            SystemsWorkedWith = new List<SystemItem>
        {
            new() { Name = "CMS", Description = "Content management" },
            new() { Name = "Float", Description = "Issuing and receiving" },
            new() { Name = "Vault", Description = "Inventory management" },
            new() { Name = "Isitaki", Description = "Credit card transaction operations" },
            new() { Name = "DRGT", Description = "Punter records, Bingo machine maintenance, employee shift records, transaction history" },
            new() { Name = "GAAP", Description = "Inventory management" },
            new() { Name = "Bingo Slots IGT", Description = "Technical support & component maintenance" },
            new() { Name = "Operational Shift Roster System", Description = "Maintenance and updates" },
            new() { Name = "Data Capturing Systems", Description = "Entry, validation, and accuracy checks" },
            new() { Name = "Presence", Description = "Employee call records" },
            new() { Name = "Einstein", Description = "Database for customer records and info retrieval" },
            new() { Name = "Inovo", Description = "Dialer operations" },
            new() { Name = "Alchemy", Description = "Debit order records" }
        },

            // ── WORK EXPERIENCE ─────────────────────────────────
            WorkExperiences = new List<WorkExperience>
        {
            new()
            {
                Role = "Customer Service & Systems Support Assistant",
                Company = "Bingo Royale Goldrush",
                Description = "Hardware/software troubleshooting, incident management, ticketing systems, and escalation handling across 13 business systems.",
                Period = "Current"
            },
            new()
            {
                Role = "Collections Agent",
                Company = "Tenacity Financial Services",
                Description = "Account management, billing dispute resolution, regulatory compliance, and accurate record keeping.",
                Period = "Previous"
            }
        },

            // ── TSIBA COURSEWORK ────────────────────────────────
            Coursework = new List<CourseModule>
        {
            new() { Code = "KM-01", Title = "Systems Analysis & Design", Covered = "SDLC, BPMN, UML, FRS" },
            new() { Code = "KM-02", Title = "Software Testing", Covered = "Black-box, white-box, test cases, RTM" },
            new() { Code = "KM-03", Title = "Deployment & Integration", Covered = "Blue-Green, Canary, integration patterns" },
            new() { Code = "KM-04", Title = "System Management", Covered = "Maintenance, change management, LORA" },
            new() { Code = "KM-05", Title = "Mathematics", Covered = "Statistics, probability, linear programming, calculus" },
            new() { Code = "KM-06", Title = "Graphs & Equations", Covered = "Linear systems, graphical methods" },
            new() { Code = "KM-07", Title = "Linear Programming", Covered = "Optimization, constraints, simplex method" },
            new() { Code = "KM-08", Title = "Inequalities", Covered = "Linear and quadratic inequalities" }
        }
        };

        ViewData["Title"] = "Siphelele Ndzundzu | Full-Stack Developer";
        return View(vm);
    }

    public IActionResult Error()
    {
        return View(new SiphelelePortfolio.UI.Models.ErrorViewModel
        {
            RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
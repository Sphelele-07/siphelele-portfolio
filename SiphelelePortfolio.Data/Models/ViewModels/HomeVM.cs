using SiphelelePortfolio.Data.Models.Domain;

namespace SiphelelePortfolio.Data.Models.ViewModels;

public class HomeVM
{
    public List<Project> Projects { get; set; } = new();
    public List<Certificate> Certificates { get; set; } = new();

    // Technical skills grouped
    public List<SkillGroup> TechnicalSkills { get; set; } = new();

    // Systems from work experience
    public List<SystemItem> SystemsWorkedWith { get; set; } = new();

    // Work history
    public List<WorkExperience> WorkExperiences { get; set; } = new();

    // TSIBA coursework
    public List<CourseModule> Coursework { get; set; } = new();
}

public class SkillGroup
{
    public string Category { get; set; } = string.Empty;
    public List<string> Items { get; set; } = new();
}

public class SystemItem
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class WorkExperience
{
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Period { get; set; }
}

public class CourseModule
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Covered { get; set; } = string.Empty;
}
using System.ComponentModel.DataAnnotations;

namespace SiphelelePortfolio.Data.Models.Domain;

public class Project
{
    public int ProjectId { get; set; }

    [Required, StringLength(100, MinimumLength = 2)]
    [Display(Name = "Project Title")]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 2)]
    [Display(Name = "Project Type")]
    public string Type { get; set; } = string.Empty;

    [Required, StringLength(200)]
    [Display(Name = "Thumbnail Image Path")]
    public string ImagePath { get; set; } = string.Empty;

    [Required, StringLength(300)]
    [Display(Name = "Tech Stack Tags (comma separated)")]
    public string Tags { get; set; } = string.Empty;

    [Required, StringLength(500, MinimumLength = 10)]
    [Display(Name = "Problem Statement")]
    public string Problem { get; set; } = string.Empty;

    [Required, StringLength(500, MinimumLength = 10)]
    [Display(Name = "Solution")]
    public string Solution { get; set; } = string.Empty;

    [Required, StringLength(500, MinimumLength = 5)]
    [Display(Name = "Result")]
    public string Result { get; set; } = string.Empty;

    [Url, StringLength(300)]
    [Display(Name = "Demo Video URL (YouTube embed)")]
    public string? DemoVideoUrl { get; set; }

    [Url, StringLength(300)]
    [Display(Name = "Live URL")]
    public string? LiveUrl { get; set; }

    [StringLength(300)]
    [Display(Name = "Requirements PDF Path")]
    public string? RequirementsPdfPath { get; set; }

    [Range(0, 100)]
    [Display(Name = "Display Order")]
    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
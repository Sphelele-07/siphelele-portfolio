using System.ComponentModel.DataAnnotations;

namespace SiphelelePortfolio.Data.Models.Domain;

public class Engagement
{
    public int EngagementId { get; set; }

    [Required, StringLength(50)]
    public string EventType { get; set; } = string.Empty;

    public int? TargetId { get; set; }

    [StringLength(200)]
    public string? TargetLabel { get; set; }

    [StringLength(300)]
    public string? UserAgent { get; set; }

    [StringLength(50)]
    public string? IpAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
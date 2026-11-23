using System.ComponentModel.DataAnnotations;

namespace SiphelelePortfolio.Data.Models.Domain;

public class Collaboration
{
    public int CollaborationId { get; set; }

    [Required, StringLength(100, MinimumLength = 2)]
    [RegularExpression(@"^[A-Za-zÀ-ÿ'\- ]+$",
        ErrorMessage = "Name may only contain letters, spaces, hyphens and apostrophes.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(200, MinimumLength = 5)]
    [Display(Name = "Idea / Subject")]
    public string Subject { get; set; } = string.Empty;

    [Required, StringLength(1000, MinimumLength = 10)]
    [Display(Name = "Message")]
    public string Message { get; set; } = string.Empty;

    public string Status { get; set; } = "New"; // New | Read | Archived
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}